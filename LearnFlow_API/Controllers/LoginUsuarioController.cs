using LearnFlow_Service.DTOs;
using LearnFlow_Service.Validadores;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LearnFlow_API.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class LoginUsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuario;

        public LoginUsuarioController(IUsuarioRepositorio usuario)
        {
            _usuario = usuario;
        }

        [HttpPost]
        public async Task<LoginDTOs> InseriLoginUsuario(string email,string senha)
        {
            return await _usuario.InserirLoginUsuario(email, senha);
        }

    }
}
