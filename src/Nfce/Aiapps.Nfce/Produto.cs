using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Produto
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Ncm { get; set; }
        public string Cfop { get; set; }
        public string Medida { get; set; }
        public string Categoria { get; set; }

        public decimal Valor { get; set; }
        public string UrlImagem { get; set; }
        public bool? EhTaxaServico { get; set; }
        public bool? EhCreditoLoja { get; set; }
        public bool? TemControleEstoque { get; set; }
        public int? MinimoEstoque { get; set; }
        public int? MaximoEstoque { get; set; }
    }
}
