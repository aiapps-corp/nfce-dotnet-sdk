using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Orders
{
    public abstract class ItemBase
    {
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal? QuantidadeMovimentacaoEstoque { get; set; } = 1;
        
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public string Uuid { get; set; }
    }
    public class Item : ItemBase
    {
        public string NCM { get; set; }
        public string Cfop { get; set; }
        public bool EhTaxa { get; set; }
        public CustomizacaoItem[] Customizacao { get; set; } = new CustomizacaoItem[0];
    }

    public class CustomizacaoItem : ItemBase
    {

    }
}
