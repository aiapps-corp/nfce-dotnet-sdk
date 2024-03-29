﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Orders
{
    public class Produto
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoBarra { get; set; }
        
        public string Nome { get; set; }
        public string Ncm { get; set; }
        public string Cfop { get; set; }
        public string Cest { get; set; }
        public string Medida { get; set; }
        public Categoria Categoria { get; set; } = new Categoria();
        public Composicao[] Composicao { get; set; } = new Composicao[0];
        public Customizacao[] Customizacao { get; set; } = new Customizacao[0];
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public string UrlImagem { get; set; }
        public bool? EhTaxaServico { get; set; }
        public bool? EhCreditoLoja { get; set; }
        public bool? TemControleEstoque { get; set; }
        public int? MinimoEstoque { get; set; }
        public int? MaximoEstoque { get; set; }
        public TaxSettings TaxSettings { get; set; } = new TaxSettings();
    }

    public class Categoria { 
        public string Id { get; set; }
        public string Nome { get; set; }
        public string UrlImagem { get; set; }
    }

    public class Composicao
    {
        public Produto Produto { get; set; }
        public bool DeveIncluirNaNota { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }
    }

    public class Customizacao
    {
        public string Titulo { get; set; }
        public decimal? Minmo { get; set; }
        public decimal? Maximo { get; set; }
        public decimal? QuantidadeOpcoesGratuitas { get; set; }
        public OpcaoCustomizacao[] Opcoes { get; private set; } = new OpcaoCustomizacao[0];
        public TipoOpcao TipoOpcao { get; set; }
    }
    public class OpcaoCustomizacao 
    {
        public virtual Produto Produto { get; set; }
        public decimal Quantidade { get; set; } = 1;
        public string DescricaoQuantidade { get; set; }
        public decimal? Valor { get; set; }
        public bool PodeSerGratuito { get; set; }
    }
    public enum TipoOpcao
    {
        Adicional = 0,
        UnicaEscolha = 1,
        MultiplaEscolha = 2,
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
