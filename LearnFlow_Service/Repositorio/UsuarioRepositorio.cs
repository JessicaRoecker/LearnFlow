using System.Data;
using Dapper;
using LearnFlow_Service.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly string? _conectionString;

    public UsuarioRepositorio(IConfiguration configuration)
    {
        _conectionString = configuration.GetConnectionString("MinhaConexao");
    }

    public async Task<LoginDTOs> InserirLoginUsuario(string email, string senha)
    {
        var login = new  LoginDTOs();
        Log.Information("Tentando registrar um novo usuário com o email {Email}", email);

        using (IDbConnection connection = new SqlConnection(_conectionString))
        {
            string query = @"
                INSERT INTO Login (email, senhaHash)
                VALUES (@email, @senha);";

            var parametros = new
            {
                Email = email,
                Senha = senha
            };
            try
            {
                login = await connection.QueryFirstOrDefaultAsync<LoginDTOs>(query, parametros);
                Log.Information("Usuario Cadastrado com sucesso");
                return login;
            }
            catch(SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Log.Information($"Já existe uma conta registrada com o email fornecido");
                    login.Erro = $"Já existe uma conta registrada com o email '{email}' fornecido.";
                    return login;
                }
                else
                    Log.Information($"ERRO: {ex.Message}");
                    throw new Exception(ex.Message);
            }
        }
    }
}
