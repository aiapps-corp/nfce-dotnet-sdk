using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Aiapps.Sdk.ProductCatalog
{
    public class Product
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string BarCode { get; set; }
        
        public string Name { get; set; }
        public string Ncm { get; set; }
        public string Cfop { get; set; }
        public string Cest { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Category Category { get; set; } = new Category();
        public Composition[] Composition { get; set; } = new Composition[0];
        public Customization[] Customizations { get; set; } = new Customization[0];
        public decimal SaleValue { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsServiceCharge { get; set; }
        public bool? IsStoreCredit { get; set; }
        public bool? HasStockControl { get; set; }
        public int? MinimumInStock { get; set; }
        public int? MaximumInStock { get; set; }
        public TaxSettings TaxSettings { get; set; } = new TaxSettings();
    }

    public class Category { 
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }

    public class Composition
    {
        public Product Product { get; set; }
        public bool ShouldShowOnTheInvoice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public string Note { get; set; }
    }

    public class Customization
    {
        public string Title { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        public decimal? FreeOptionsQuantity { get; set; }
        public CustomizationOption[] Options { get; private set; } = new CustomizationOption[0];
        public ProductCustomizationOptionsType OptionsType { get; set; }
    }
    public class CustomizationOption
    {
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; } = 1;
        public string QuantityLabel { get; set; }
        public decimal? Value { get; set; }
        public bool CanBeFree { get; set; }
    }
    public enum ProductCustomizationOptionsType
    {
        [XmlEnum(Name = "Adição")]
        Addition = 0,
        [XmlEnum(Name = "Uma opção")]
        OneChoice = 1,
        [XmlEnum(Name = "Multiplas opções")]
        MultipleChoice = 2,
    }

    public class TaxSettings
    {
        public string Cfop { get; set; }
        public InternalSettings Settings { get; set; } = new InternalSettings();

        public class InternalSettings
        {
            public Ipi Ipi { get; set; } = new Ipi();
            public Pis Pis { get; set; } = new Pis();
            public Cofins Cofins { get; set; } = new Cofins();
            public Icms Icms { get; set; } = new Icms();
        }

        public class Ipi
        {
            public string Cst { get; set; }
            public decimal? Aliquot { get; set; }
        }
        public class Pis
        {
            public string Cst { get; set; }
            public decimal? Aliquot { get; set; }
        }
        public class Cofins
        {
            public string Cst { get; set; }
            public decimal? Aliquot { get; set; }
        }
        public class Icms
        {
            public IcmsState[] States { get; set; } = new IcmsState[0];
        }
        public class IcmsState
        {
            public string State { get; set; }
            public string Cst { get; set; }

            public bool IsException { get; set; }
            public decimal? Aliquot { get; set; }
            public decimal? AliquotSt { get; set; }
            public decimal? AliquotMva { get; set; }
            public decimal? AliquotIcmsNonContributors { get; set; }
            public decimal? AliquotInternalUfDestination { get; set; }
            public decimal? PercentageDeferral { get; set; }
            public decimal? PercentageReductionBc { get; set; }
            public decimal? PercentageReductionBcSt { get; set; }
        }
    }
}
