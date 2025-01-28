using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSMIA_api.Models;
using Microsoft.Extensions.Configuration;
using CSMIA_api.RestGateway;

namespace CSMIA_api.Controllers
{
    public class FlightController : Controller
    {
        private readonly IConfiguration _configuration;

        public FlightController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<FlightData> flightDataList = await FetchFlightDataFromAdaniApi();

            // Pass the data to the view
            return View(flightDataList);
        }

        private async Task<List<FlightData>> FetchFlightDataFromAdaniApi()
        {
            // Fetch API settings from appsettings.json
            string apiUrl = _configuration["ApiSettings:ApiUrl"];
            string username = _configuration["ApiSettings:Username"];
            string password = _configuration["ApiSettings:Password"];

            // Use RestGatewayManager to send the request
            var response = await RestGatewayManager.GetAsync<List<FlightData>>(apiUrl, null, username, password);

            if (response.IsSuccessful)
            {
                return response.ResultData;
            }
            else
            {
                // Handle error (e.g., log it)
                return new List<FlightData>(); // Return an empty list or handle as needed
            }
        }
    }
}
