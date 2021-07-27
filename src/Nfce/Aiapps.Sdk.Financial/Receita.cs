using Aiapps.Sdk.Crm;
using System;

namespace Aiapps.Sdk.Financial
{
    public class Receita
    {
        public string Referencia { get; set; }
        public string Descricao { get; set; } = "Receita de venda";
        public DateTime DataCriacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public DateTime? DataCompetencia { get; set; }

        public decimal Valor { get; set; }
        public decimal Taxa { get; set; }
        public decimal? ValorAReceber { get; set; }
        public Cliente Cliente { get; set; }
        public string Observacao { get; set; }
        public Situacao Situacao { get; set; }
        public string ComecoCartao { get; set; }
        public string FinalCartao { get; set; }
        public string Bandeira { get; set; }
        /// <summary>
        /// Forma de pagamento:
        /// 01=Dinheiro 
        /// 02=Cheque 
        /// 03=Cartão de Crédito 
        /// 04=Cartão de Débito 
        /// 05=Crédito Loja 
        /// 10=Vale Alimentação 
        /// 11=Vale Refeição 
        /// 12=Vale Presente 
        /// 13=Vale Combustível 
        /// 15=Boleto Bancário
        /// 16=Depósito Bancário
        /// 17=Pagamento Instantâneo(PIX)
        /// 18=Transferência bancária, Carteira Digital
        /// 19=Programa de fidelidade, Cashback, Crédito Virtual
        /// 90=Sem pagamento
        /// 99=Outros
        /// </summary>
        public string FormaPagamento { get; set; }
        public ContaBancaria ContaBancaria { get; set; } = new ContaBancaria();
        public string ReferenciaVenda { get; set; }
        public Equipamento Equipamento { get; set; } = new Equipamento();
    }
}
