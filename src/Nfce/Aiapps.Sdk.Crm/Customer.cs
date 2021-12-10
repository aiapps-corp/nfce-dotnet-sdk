using System;

namespace Aiapps.Sdk.Crm
{
    [Obsolete]
    public class Cliente
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Gender Sexo { get; set; } = Gender.None;
        public DateTime? DataNascimento { get; set; }
        public Endereco Endereco { get; set; } = new Endereco();

        public string Conta { get; set; }
        public string TagConta { get; set; }
        public string Titular { get; set; }
    }
    public class Customer
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public DateTime? Birthday { get; set; }
        public Address Address { get; set; } = new Address();

        public string Account { get; set; }
        public string AccountTag { get; set; }
        public string Holder { get; set; }
    }

    public enum Gender
    {
        None = 0,
        Male = 1,
        Female = 2,
    }
}
