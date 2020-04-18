using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Cliente
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; } = new Endereco();

        public string Conta { get; set; }
        public string TagConta { get; set; }
        public string Titular { get; set; }
    }
}
