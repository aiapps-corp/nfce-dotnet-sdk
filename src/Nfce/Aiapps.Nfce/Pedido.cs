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
        public Entrega Entrega { get; set; } = new Entrega();
        public decimal Desconto { get; set; }
        public string ContaCliente { get; set; }
        public string Referencia { get; set; }
    }

    public class Entrega
    {
        public decimal Frete { get; set; }
        public TipoFrete TipoFrete { get; set; } = TipoFrete.Gratuito;
        public Transportador Transportador { get; set; } = new Transportador();
        public Endereco Endereco { get; set; } = new Endereco();
    }
    public enum TipoFrete
    {
        Emitente = 0,
        Destinatario = 1,
        Terceiros = 2,
        Gratuito = 9,
    }
}
