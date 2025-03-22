using LearnFlow_Service.DTOs;

public interface IUsuarioRepositorio
{
    Task<LoginDTOs> EmailUsuario(string email, string senha);
    Task<LoginDTOs> InserirLoginUsuario(string email, string senha, string nome);
    bool VerificarSenha(string senha, string hash);
}