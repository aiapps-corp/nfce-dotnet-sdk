using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Pagamento
    {
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public Cartao Cartao { get; set; }
        public string Referencia { get; set; }
    }
}
