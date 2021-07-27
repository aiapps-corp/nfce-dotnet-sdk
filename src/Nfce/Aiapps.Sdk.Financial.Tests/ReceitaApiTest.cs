using Aiapps.Sdk.Financial.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Financial.Tests
{
    [TestClass]
    public class ReceitaApiTest
    {
        [TestMethod]
        public async Task Post_Test()
        {
            var receitaApi = new ReceitaApi(ValidCredencial);
            var result = await receitaApi.Post(new Receita { 
                Bandeira = "visa",
                Cliente = new Crm.Cliente {
                    Nome = "João"
                },
                ComecoCartao = "9999",
                ContaBancaria = new ContaBancaria { },
                DataCompetencia = DateTime.Today,
                DataCriacao = DateTime.UtcNow,
                DataRecebimento = DateTime.Today.AddDays(30),
                DataVencimento = DateTime.Today.AddDays(30),
                Descricao = "Receita de venda",
                Equipamento = new Equipamento { },
                FinalCartao = "9999",
                Observacao = "",
                Referencia = Guid.NewGuid().ToString(),
                ReferenciaVenda = Guid.NewGuid().ToString(),
                Situacao = Situacao.Autorizado,
                Taxa = 1,
                FormaPagamento = "03",
                Valor = 10,
                ValorAReceber = 9,
            });
            Assert.IsTrue(result.Sucesso);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "teste@aiapps.com.br",
            Senha = "123456"
        };
    }
}
