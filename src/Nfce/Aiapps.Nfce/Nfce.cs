using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Nfce
    {
        public string Numero { get; set; }
        public string Serie { get; set; }
        public DateTime? DataHora { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public Item[] Itens { get; set; } = new Item[0];
        public Pagamento[] Pagamentos { get; set; } = new Pagamento[0];
        public Equipamento PontoVenda { get; set; }
        public Vendedor Vendedor { get; set; }
        public decimal Desconto { get; set; }
        public bool? EhManual { get; set; }
        public string Referencia { get; set; }
    }
}
