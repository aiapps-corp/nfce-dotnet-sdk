﻿using Aiapps.Sdk.Crm;
using System;

namespace Aiapps.Sdk.Orders
{
    public class Pedido
    {
        public const string CFOP_BONIFICACAO = "5.910";
        public const string CFOP_PERDA = "5.927";
        public string Numero { get; set; }
        public string Serie { get; set; }
        public DateTime? DataHora { get; set; }
        public string Cfop { get; set; }
        public Cliente Cliente { get; set; } = new Cliente();
        public Canal Canal { get; set; } = new Canal();
        public Vendedor Vendedor { get; set; } = new Vendedor();
        public Equipamento PontoVenda { get; set; } = new Equipamento();
        public Item[] Itens { get; set; } = new Item[0];
        public Pagamento[] Pagamentos { get; set; } = new Pagamento[0];
        public EntregaPedido Entrega { get; set; } = new EntregaPedido();
        public IndicadorIntermediador? IndicadorIntermediador { get; set; }
        public IntermediadorTransacao IntermediadorTransacao { get; set; } = new IntermediadorTransacao();
        public decimal Desconto { get; set; }
        public string ContaCliente { get; set; }
        public string Referencia { get; set; }
        public string Situacao { get; set; }
        public bool Assincrono { get; set; }
    }

    /// <summary>
    /// indIntermed - Indicador de intermediador/marketplace 
    /// 0=Operação sem intermediador(em site ou plataforma própria)
    /// 1=Operação em site ou plataforma de terceiros(intermediadores/marketplace)
    /// </summary>
    public enum IndicadorIntermediador
    {
        /// <summary>
        /// 0-Operação sem intermediador(em site ou plataforma própria)
        /// </summary>
        SemIntermediador = 0,

        /// <summary>
        /// 1-Operação em site ou plataforma de terceiros(intermediadores/marketplace)
        /// </summary>
        ComIntermediador = 1,
    }
}
