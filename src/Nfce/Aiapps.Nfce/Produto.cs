using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Produto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public string Categoria { get; set; }
        public string NCM { get; set; }
        public string Cfop { get; set; }
        public bool? TemControleEstoque { get; set; }
        public int? MinimoEstoque { get; set; }
        public int? MaximoEstoque { get; set; }
        public string Imagem { get; set; }
        public string Referencia { get; set; }
    }
}
