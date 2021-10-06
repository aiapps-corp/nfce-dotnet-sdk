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
}
