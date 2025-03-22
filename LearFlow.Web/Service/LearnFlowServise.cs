using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
namespace LearFlow.Web.Service
{
    public class LearnFlowServise
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public LearnFlowServise(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _apiUrl = configuration.GetValue<string>("ApiUrl");
            client.BaseAddress = new Uri(_apiUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IActionResult> Login(StringContent payload)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiUrl}api/LoginUsuario/Login", payload);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new OkObjectResult(responseJson);
                }
                else
                {
                    return new ObjectResult(responseJson)
                    {
                        StatusCode = (int)response.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult($"Internal server error: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<IActionResult> Cadastro(StringContent payload)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync($"{_apiUrl}api/LoginUsuario/Cadastrar", payload);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new OkObjectResult(responseJson);
                }
                else
                {
                    return new ObjectResult(responseJson)
                    {
                        StatusCode = (int)response.StatusCode
                    };
                }
            }
            catch(Exception ex)
            {
                return new OkObjectResult($"Internal server error: {ex.Message}")
                {
                    StatusCode = 500
                };
            }
           
        }
    }
}