using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Invoices
{
    public class CancelarNf
    {
        public string Referencia { get; set; }
        public string Chave { get; set; }
        public string Motivo { get; set; }
    }
    public class RetornoCancelamento
    {
        public string Protocolo { get; set; }
    }
}
