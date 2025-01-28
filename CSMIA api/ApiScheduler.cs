using CSMIA_api.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CSMIA_api
{
    public class ApiScheduler : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IConfiguration _configuration;
        public ApiScheduler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CallFlightFeedAPI, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        private async void CallFlightFeedAPI(object state)
        {
            List<FlightData> flightDataList = FetchFlightDataFromFlightFeedApi();
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
                    List<FlightData> flightData = JsonConvert.DeserializeObject<List<FlightData>>(responseBody);
                    return flightData;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
            return new List<FlightData>();
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