using Aiapps.Sdk.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Invoices
{
    public abstract class Nf
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Numero { get; set; }
        public string Serie { get; set; }
        public string ChaveAcesso { get; set; }
        public string Situacao { get; set; }
        public string Cliente { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public Item[] Itens { get; set; } = new Item[0];
        public string Url { get; set; }
        public Sefaz Sefaz { get; set; } = new Sefaz();

        public string Referencia { get; set; }
        public string Erro { get; set; }
    }
    public class InvoiceInfo
    {
        public string AccessKey { get; set; }
        public string Protocol { get; set; }
    }

}
