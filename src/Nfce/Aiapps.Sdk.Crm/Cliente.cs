using System;

namespace Aiapps.Sdk.Crm
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
