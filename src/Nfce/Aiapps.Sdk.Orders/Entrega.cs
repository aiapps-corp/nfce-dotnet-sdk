using Aiapps.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Orders
{
    public class Entrega
    {
        public Entregador Entregador { get; set; }
        public Equipamento PontoEntrega { get; set; }
        public string ReferenciaPedido { get; set; }
        public string ProdutoId { get; set; }
        public DateTime Data { get; set; }
    }

    public class EntregaPedido
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
        TransporteProprioPorContaDoRemetente = 3,
        TransporteProprioPorContaDoDestinatario = 4,
        Gratuito = 9,
    }
}
