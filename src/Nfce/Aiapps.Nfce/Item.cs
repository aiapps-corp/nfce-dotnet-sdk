using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public class Item
    {
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public string NCM { get; set; }
        public string Cfop { get; set; }
        public bool EhTaxa { get; set; }
        public CustomizacaoItem[] Customizacao { get; set; } = new CustomizacaoItem[0];
    }

    public class CustomizacaoItem
    {
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }

    }
}
