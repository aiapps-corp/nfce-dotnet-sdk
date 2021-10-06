namespace Aiapps.Sdk.Financial
{
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
}
