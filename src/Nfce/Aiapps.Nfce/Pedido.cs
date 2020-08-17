using Aiapps.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Pedido
    {
        public string Numero { get; set; }
        public string Serie { get; set; }
        public DateTime? DataHora { get; set; }
        public string Cfop { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public string TipoPagamento { get; set; }
        public Vendedor Vendedor { get; set; } = new Vendedor();
        public Equipamento PontoVenda { get; set; } = new Equipamento();
        public Item[] Itens { get; set; } = new Item[0];
        public Pagamento[] Pagamentos { get; set; } = new Pagamento[0];
        public EntregaPedido Entrega { get; set; } = new EntregaPedido();
        public decimal Desconto { get; set; }
        public string ContaCliente { get; set; }
        public string Referencia { get; set; }
        public string Situacao { get; set; }
    }
}
