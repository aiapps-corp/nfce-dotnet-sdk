using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk
{
    [Obsolete("Use Device")]
    public class Equipamento
    {
        /// <summary>
        /// Identificador do pagamento
        /// </summary>
        public string Codigo { get; set; }
        public string MacAddress { get; set; }
        public string Nome { get; set; }

        /// <summary>
        /// Identificador do terminal de pagamento
        /// </summary>
        public string IdTerminalPagamento { get; set; }
    }
}
