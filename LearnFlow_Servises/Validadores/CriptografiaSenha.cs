using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Serilog;

namespace LearnFlow_Service.Validadores
{
    public static class CriptografiaSenha
    {
        // Gerar um hash seguro da senha com BCrypt
        public static string GerarHash(this string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        // Verificar se a senha digitada confere com o hash armazenado
        public static bool ConferirSenha(string senha, string hash)
        {
            bool senhaUsuario = false;
            try
            {
                senhaUsuario = BCrypt.Net.BCrypt.Verify(senha, hash);

                if (senhaUsuario)
                    return true;
                else
                {
                    throw new Exception("Senha Incorreta");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

