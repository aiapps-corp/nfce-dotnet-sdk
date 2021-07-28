using System.Xml.Serialization;

namespace Aiapps.Sdk.Hooks
{
    public enum EventType
    {
        [XmlEnum("Produto atualizado")]
        ProductUpdated = 1000,
        [XmlEnum("Estoque do produto esgotado")]
        DepletedProductStock = 10000,
        [XmlEnum("Reposição de estoque do produto")]
        ProductStockReplenishment = 10001,


        [XmlEnum("Nota fiscal autorizada")]
        InvoiceAutorized = 2000,
    }
}
