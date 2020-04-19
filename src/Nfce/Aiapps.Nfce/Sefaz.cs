using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Sefaz
    {
        public DateTime EmitidoEm { get; set; }
        public string Protocolo { get; set; }
        public string Url { get; set; }
        public string Motivo { get; internal set; }
        public string CodigoStatus { get; internal set; }
    }
}
