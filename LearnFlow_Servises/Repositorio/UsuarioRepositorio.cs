using System.Data;
using Dapper;
using LearnFlow_Service.DTOs;
using LearnFlow_Service.Validadores;
using Microsoft.Data.SqlClient;
using Serilog;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly string? _conectionString;

    public UsuarioRepositorio(IConfiguration configuration)
    {
        _conectionString = configuration.GetConnectionString("MinhaConexao");
    }

    public async Task<LoginDTOs> InserirLoginUsuario(string email, string senha, string nome)
    {
        Log.Information("Inicio do métedo InserirLoginUsuario");
        var login = new LoginDTOs();

        senha = CriptografiaSenha.GerarHash(senha);

        using (IDbConnection connection = new SqlConnection(_conectionString))
        {
            string query = @"
                INSERT INTO Login (email, senhaHash, nome)
                VALUES (@email, @senha,@nome);";

            var parametros = new
            {
                Email = email,
                Senha = senha,
                Nome = nome
            };
            try
            {
                login = await connection.QueryFirstOrDefaultAsync<LoginDTOs>(query, parametros);
                Log.Information("Usuario Cadastrado com sucesso");
                Log.Information("Final do métedo InserirLoginUsuario");

                return login;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    Log.Information($"Já existe uma conta registrada com o email fornecido");
                    throw new Exception("Já existe uma conta registrada com o email fornecido");
                }
                else
                {
                    Log.Error($"ERRO: {ex.Message}");
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public async Task<LoginDTOs> EmailUsuario(string email, string senhaHash)
    {
        Log.Information("Inicio do metedo AcessarLoginUsuario");
        var login = new LoginDTOs();
        using (IDbConnection connection = new SqlConnection(_conectionString))
        {
            string query = @"Select email, senhaHash from Login where email = @email";
            try
            {
                login = await connection.QueryFirstOrDefaultAsync<LoginDTOs>(query, new { email});

                if (login != null)
                {
                    string queryUpdate = @"UPDATE Login SET ultimoLogin = GETDATE() WHERE email = @email";
                    await connection.QueryFirstOrDefaultAsync<LoginDTOs>(queryUpdate, new { email });
                    Log.Information("Final do metedo AcessarLoginUsuario");
                    return login;
                }
                else
                {
                    throw new Exception("Email não cadastrado");
                }

            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message);
                throw new Exception("Erro inesperado no servidor!");

            }
        }
    }

    public bool VerificarSenha(string senha, string hash)
    {
        Log.Information("Inicio do métedo VerificarSenha");
        var conferirSenha = CriptografiaSenha.ConferirSenha(senha, hash);
        Log.Information("Final do métedo VerificarSenha");
        return conferirSenha;
    }
}
