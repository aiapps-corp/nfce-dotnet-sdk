using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Invoices
{
    public class Sefaz
    {
        public DateTime EmitidoEm { get; set; }
        public DateTime? CanceladoEm { get; set; }
        public string Protocolo { get; set; }
        public string Url { get; set; }
        public string Motivo { get; set; }
        public string CodigoStatus { get; set; }
    }
}
