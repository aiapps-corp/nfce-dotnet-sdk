using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Entrega
    {
        public Entregador Entregador { get; set; }
        public Equipamento PontoEntrega { get; set; }
        public string ReferenciaPedido { get; set; }
        public string ProdutoId { get; set; }
        public DateTime Data { get; set; }
    }
}
