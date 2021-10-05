using Aiapps.Sdk.Crm;
using System;

namespace Aiapps.Sdk.Financial
{
    public class Revenue
    {
        public string Reference { get; set; }
        public string Description { get; set; } = "Receita de venda";
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompetenceDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal? AmountPaid { get; set; }
        public Customer Customer { get; set; }
        public string Note { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentCardStartNumber { get; set; }
        public string PaymentCardFinalNumber { get; set; }
        public CreditCardType CreditCardType { get; set; }

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
        public FormOfPaymentType FormOfPaymentType { get; set; }
        public BankAccount BankAccount { get; set; } = new BankAccount();
        public string OrderReference { get; set; }
        public Device PaymentDevice { get; set; } = new Device();
    }

    public enum CreditCardType
    {
        none,
        visa,
        mastercard,
        amex,
        discover,
        maestro,
        elo,
        hipercard,
        diners,
        jcb,
        aura,
        cabal,
        other,
    }
    /// <summary>
    /// Form Of Payment:
    /// 01=Money 
    /// 02=Check 
    /// 03=Credi tCard
    /// 04=Debit Card
    /// 05=Store Credit
    /// 10=Valley of Food 
    /// 11=Meal Vouche
    /// 12=Gift Certificate
    /// 13=Fuel Valley 
    /// 14=Duplicata Mercantil 
    /// 15=Boleto Bancário
    /// 16=Depósito Bancário 
    /// 17=Pagamento Instantâneo(PIX)
    /// 18=Transferência bancária, Carteira Digital
    /// 19=Programa de fidelidade, Cashback, Crédito Virtual
    /// 90=WithoutPay
    /// 99=Other
    /// </summary>
    public enum FormOfPaymentType
    {
        /// <summary>
        /// 01=Money 
        /// </summary>
        Money = 1,
        /// <summary>
        /// 02=Check
        /// </summary>
        Check = 2,
        /// <summary>
        /// 03=Credit Card
        /// </summary>
        CreditCard = 3,
        /// <summary>
        /// 04=Debit Card
        /// </summary>
        DebitCard = 4,
        /// <summary>
        /// 05=Store Credit
        /// </summary>
        StoreCredit = 5,
        /// <summary>
        /// 10=Valley of Food
        /// </summary>
        ValleyOfFood = 10,
        /// <summary>
        /// 11=Meal Vouche
        /// </summary>
        MealVouche = 11,
        /// <summary>
        /// 12=Gift Certificate
        /// </summary>
        GiftCertificate = 12,
        /// <summary>
        /// 13=Fuel Valley 
        /// </summary>
        FuelValley = 13,
        /// <summary>
        /// 14=MerchantDuplicate 
        /// </summary>
        MerchantDuplicate = 14,
        /// <summary>
        /// 15=Bankslip 
        /// </summary>
        Bankslip = 15,

        /// <summary>
        /// 16=Depósito Bancário
        /// </summary>
        BankDeposit = 16,
        /// <summary>
        /// 17=Pagamento Instantâneo(PIX)
        /// </summary>
        PIX = 17,
        /// <summary>
        /// 18=Transferência bancária, Carteira Digital
        /// </summary>
        BankTransfer = 18,
        /// <summary>
        /// 19=Programa de fidelidade, Cashback, Crédito Virtual
        /// </summary>
        Cashback = 19,

        /// <summary>
        /// 90=WithoutPay 
        /// </summary>
        WithoutPay = 90,
        /// <summary>
        /// 99=Other
        /// </summary>
        Other = 99,
    }

    [Obsolete]
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
