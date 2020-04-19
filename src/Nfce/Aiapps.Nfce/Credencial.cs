using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Credencial
    {
        public Credencial() : this(null, null) { }
        public Credencial(string email, string senha) {
            Email = email;
            Senha = senha;
        }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
    }
}
