using LearnFlow_Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LearnFlow_API.Controllers
{
    [Route("api/LoginUsuario")]
    [ApiController]
    public class LoginUsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuario;

        public LoginUsuarioController( IUsuarioRepositorio usuario)
        {
            _usuario = usuario;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> InseriLoginUsuario([FromBody] LoginDTOs body)
        {
            try
            {
                var inserirUsuario = await _usuario.InserirLoginUsuario(body.Email, body.SenhaHash, body.Nome);

                return Ok(new { message = "Cadastro realizado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> AcessarLoginUsuario([FromBody] LoginDTOs body)
        {
            try
            {
                var usuario = await _usuario.EmailUsuario(body.Email, body.Email);
                bool verificarSenha = _usuario.VerificarSenha(body.SenhaHash, usuario.SenhaHash);
                return Ok(usuario);
            }
            catch(Exception ex) { return BadRequest(new {   message = ex.Message}); }
        }
    }
}
