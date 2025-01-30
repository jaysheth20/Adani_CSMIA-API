using CSMIA_api.Models;
using Dapper;
using Hangfire.Storage;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CSMIA_api
{
    public class ApiScheduler : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IConfiguration _configuration;
        string conn = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        int schedulerExecutionTime = 2; // default time
        public ApiScheduler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_configuration["ApiSettings:SchedulerExecutionTimeInMin"]))
            {
                schedulerExecutionTime = System.Convert.ToInt32(_configuration["ApiSettings:SchedulerExecutionTimeInMin"]);
            }
            _timer = new Timer(CallFlightFeedAPI, null, TimeSpan.Zero, TimeSpan.FromMinutes(schedulerExecutionTime));
            return Task.CompletedTask;
        }

        private async void CallFlightFeedAPI(object state)
        {
            List<FlightData> flightDataList = FetchFlightDataFromFlightFeedApi();
            SaveFlightFeedData(flightDataList);
        }

        private List<FlightData> FetchFlightDataFromFlightFeedApi()
        {
            // Fetch API settings from appsettings.json
            string apiUrl = _configuration["ApiSettings:ApiUrl"];
            string username = _configuration["ApiSettings:Username"];
            string password = _configuration["ApiSettings:Password"];

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Create the Basic Authentication header
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.Timeout = TimeSpan.FromSeconds(120);
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    List<FlightData> flightData = JsonSerializer.Deserialize<List<FlightData>>(responseBody);
                    return flightData;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
            return new List<FlightData>();
        }

        private void SaveFlightFeedData(List<FlightData> flightList)
        {
            using (var connection = new SqlConnection(conn))
            {
                connection.Open();
                SaveFlightData(FilterFlightData(flightList, "DA", "S"), "ArrivDomCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "IA", "S"), "ArrivInterCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "DA", "F"), "CArrivDomCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "IA", "F"), "CArrivInterCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "DD", "F"), "CDepDomCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "ID", "F"), "CDepInterCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "DD", "S"), "DepDomCurrent", connection);
                SaveFlightData(FilterFlightData(flightList, "ID", "S"), "DepInterCurrent", connection);
            }
        }

        private List<FlightData> FilterFlightData(List<FlightData> flightList, string nature, string qualifier)
        {
            if (flightList == null || flightList.Count == 0)
            {
                return new List<FlightData>();
            }
            return flightList.Where(x => x.Nature == nature && x.Qualifier == qualifier).ToList();
        }

        private DynamicParameters SetDynamicParam(string jsonData,string tableName)
        {
            List<SetParameters> parameters = new List<SetParameters>()
            {
                new SetParameters() { ParameterName = "@flightFeedData", Value = jsonData },
                new SetParameters() { ParameterName = "@TableName", Value = tableName },
            };

            DynamicParameters dbparameters = new DynamicParameters();
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var param in parameters)
                {
                    dbparameters.Add(param.ParameterName, param.Value.ToString());
                }
            }

            return dbparameters;
        }

        private void SaveFlightData(List<FlightData> data, string tableName, SqlConnection connection)
        {
            if (data != null && data.Count > 0)
            {
                string jsonData = JsonSerializer.Serialize(data);
                DynamicParameters param = SetDynamicParam(jsonData, tableName);
                connection.Execute("InsertOrUpdateFlightData_SP", param, commandType: CommandType.StoredProcedure);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}