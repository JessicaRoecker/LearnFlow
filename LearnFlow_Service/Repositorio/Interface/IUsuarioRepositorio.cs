using LearnFlow_Service.DTOs;

public interface IUsuarioRepositorio
{
    Task<LoginDTOs> InserirLoginUsuario(string email, string senha);
}