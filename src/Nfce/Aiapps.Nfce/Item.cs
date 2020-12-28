using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Nfce
{
    public abstract class ItemBase
    {

        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public string NCM { get; set; }
        public string Cfop { get; set; }
        public string Uuid { get; set; }
    }
    public class Item : ItemBase
    {
        public bool EhTaxa { get; set; }
        public CustomizacaoItem[] Customizacao { get; set; } = new CustomizacaoItem[0];
    }

    public class CustomizacaoItem : ItemBase
    {

    }
}
