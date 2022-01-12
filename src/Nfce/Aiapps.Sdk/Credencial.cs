using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk
{
    public class Credential
    {
        public Credential() : this(null, null) { }
        public Credential(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
