using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Orders
{
    public abstract class ItemBase
    {
        private decimal _valorUnitario;
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal? QuantidadeMovimentacaoEstoque { get; set; } = 1;
        
        public decimal ValorUnitario {
            get { return _valorUnitario; }
            set { _valorUnitario = decimal.Round(value, 2); }
        }
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
