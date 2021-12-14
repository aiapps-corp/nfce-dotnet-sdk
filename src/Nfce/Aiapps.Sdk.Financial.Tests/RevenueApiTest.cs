using Aiapps.Sdk.Financial.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Financial.Tests
{
    [TestClass]
    public class RevenueApiTest
    {
        [TestMethod]
        public async Task Post_Test()
        {
            var receitaApi = new RevenueApi(ValidCredencial);
            var result = await receitaApi.Post(new Revenue
            {
                CreditCardType = CreditCardType.visa,
                Customer = new Crm.Customer
                {
                    Name = "João"
                },
                PaymentCardStartNumber = "9999",
                PaymentCardFinalNumber = "9999",
                BankAccount = new BankAccount { },
                CompetenceDate = DateTime.Today,
                CreatedAt = DateTime.UtcNow,
                PaymentDate = DateTime.Today.AddDays(30),
                DueDate = DateTime.Today.AddDays(30),
                Description = "Receita de venda",
                PaymentDevice = new Device { },
                Note = "",
                Reference = Guid.NewGuid().ToString(),
                OrderReference = Guid.NewGuid().ToString(),
                PaymentStatus = PaymentStatus.Approved,
                Discount = 1,
                FormOfPaymentType = FormOfPaymentType.CreditCard,
                Value = 10,
                AmountPaid = 9,
            });
            Assert.IsTrue(result.Sucesso);
        }
        [TestMethod]
        public async Task Delete_Test()
        {
            var receitaApi = new RevenueApi(ValidCredencial);
            var result = await receitaApi.Delete("89150c4b-bd9f-419f-b164-a2a35ce9c278");
            Assert.IsTrue(result.Sucesso);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "teste@aiapps.com.br",
            Senha = "123456"
        };
    }
}
