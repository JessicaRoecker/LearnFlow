using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFlow_Service.DTOs
{
    public class Login
    {
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty; 
        public string Nome { get; set; } = string.Empty;

    }
}
