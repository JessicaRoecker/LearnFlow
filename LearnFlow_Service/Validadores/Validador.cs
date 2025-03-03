using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LearnFlow_Service.Validadores
{
    public class Validador : IValidador
    {
        public bool ValidarSenha(string senha)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+={}\[\]:;'\""<>,.?/\\|-]).{8,}$");

            return regex.IsMatch(senha);
        }

        public bool ValidarSenhaConfirmacao(string senha, string senhaConfirmacao)
        {
            if (senhaConfirmacao == senha) 
                return true;          
            else 
                return false;
        }
    }
}
