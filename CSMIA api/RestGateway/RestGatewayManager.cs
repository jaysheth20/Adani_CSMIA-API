using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace CSMIA_api.RestGateway
{
    public static class RestGatewayManager
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<RestResponse<T>> GetAsync<T>(string endpoint, Dictionary<string, string> queryParameter, string username, string password)
        {
            try
            {
                var queryString = queryParameter != null
                    ? "?" + string.Join("&", queryParameter.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"))
                    : string.Empty;

                var requestUrl = $"{endpoint}{queryString}";

                // Create the request message
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                // Set up Basic Authentication
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                // Send the request and get the response
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                // Parse the response content
                var content = await response.Content.ReadAsStringAsync();

                var result = System.Text.Json.JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new RestResponse<T>
                {
                    ResultData = result,
                    IsSuccessful = true,
                    StatusCode = response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new RestResponse<T>
                {
                    IsSuccessful = false,
                    ErrorMessage = ex.Message
                };
            }
        }
        public static async Task<RestResponse<TOut>> PostAsync<TIn, TOut>(string endpoint, TIn postData, string username, string password)
        {
            try
            {
                // Serialize the data to JSON
                var jsonData = System.Text.Json.JsonSerializer.Serialize(postData);

                // Set up the HTTP content
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Create the request message
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = content
                };

                // Set up Basic Authentication
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                // Send the request and get the response
                var response = await _httpClient.SendAsync(request);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Parse the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                var result = System.Text.Json.JsonSerializer.Deserialize<TOut>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return new RestResponse<TOut>
                {
                    ResultData = result,
                    IsSuccessful = true,
                    StatusCode = response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new RestResponse<TOut>
                {
                    IsSuccessful = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}
