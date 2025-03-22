using LearnFlow_Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow_Web.wwwroot.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string? _URL;

        public HomeController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _URL = configuration.GetConnectionString("ApiUrl");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTOs login)
        {
            string url = $"{_URL}LoginUsuario";  // Sua URL do serviço

            // Enviar a requisição para o serviço com o corpo no formato JSON
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, login);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            // Processar mensagens de erro no caso de falha
            string errorMessage = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(errorMessage))
                return BadRequest(errorMessage);

            return BadRequest("Erro ao acessar o serviço de login.");
        }

    }
}
