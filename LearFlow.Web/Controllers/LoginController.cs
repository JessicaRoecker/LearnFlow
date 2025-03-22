using System.Text;
using System.Text.Json;
using LearFlow.Web.Service;
using LearnFlow_Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearFlow.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LearnFlowServise _learnFlowService;

        public LoginController(LearnFlowServise learnFlowServise)
        {
            _learnFlowService = learnFlowServise;
        }

        [Route("~/Autenticacao")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login body)
        {
            ClearSession();

            var login = new Login
            {
                Email = body.Email,
                SenhaHash = body.SenhaHash
            };

            var jsonString = JsonSerializer.Serialize(login);
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _learnFlowService.Login(payload);

            return response;
        }

        [Route("~/Cadastro")]
        [HttpPost]
        public async Task<IActionResult> Cadastro([FromBody] Login body)
        {
            ClearSession();

            var login = new Login
            {
                Email = body.Email,
                SenhaHash = body.SenhaHash,
                Nome = body.Nome
            };

            var jsonString = JsonSerializer.Serialize(login);
            var payload = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _learnFlowService.Cadastro(payload);

            return response;
        }

        public void ClearSession()
        {
            HttpContext.Session.Clear();  // Limpa todos os dados armazenados na sessão
        }

    }
}
