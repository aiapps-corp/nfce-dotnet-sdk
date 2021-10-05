using System;

namespace Aiapps.Sdk.Financial
{
    [Obsolete]
    public class ContaBancaria
    {
        public string Referencia { get; set; }
        public string Nome { get; set; }
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string DigitoConta { get; set; }
        public string Documento { get; set; }
    }
    public class BankAccount
    {
        public string Reference { get; set; }
        public string Name { get; set; }
        public string BankCode { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
    }
}
