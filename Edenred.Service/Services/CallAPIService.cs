using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.Service.Services
{
    public class CallAPIService
    {
        private readonly HttpClient _httpClient;
        public CallAPIService()
        {
            _httpClient = new HttpClient();
            // You might want to set some default headers, base address, etc.
            //_httpClient.BaseAddress = new Uri("https://api.example.com/");
            // For example, if the API requires JSON content
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetDataFromApi(string endpoint)
        {
            // Make a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            // Throw an exception if the request was not successful
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<string> PostDataToApi(string endpoint, string data)
        {
            string responseBody = "";
            try
            {
                // Prepare the content for the POST request
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                // Make a POST request to the API
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                // Throw an exception if the request was not successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                 responseBody = await response.Content.ReadAsStringAsync();

                
            }
            catch(Exception ex)
            {

            }
            return responseBody;
        }
    }
}
