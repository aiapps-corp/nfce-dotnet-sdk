using Aiapps.Sdk.Nfe.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Aiapps.Sdk.Orders;

namespace Aiapps.Sdk.Nfce.Tests
{
    [TestClass]
    public class NfeApiTest
    {
        [TestMethod]
        public async Task EmitirAsync_Credencial_Valida_Test()
        {
            var nfeApi = new NfeApi(ValidCredential);
            var response = await nfeApi.EmitirAsync(new Pedido
            {
                Referencia = Guid.NewGuid().ToString(),
                Itens = new Item[] {
                    new Item {
                        Cfop = "5.102",
                        NCM = "2203.00.00",
                        ProdutoId = "1",
                        ProdutoNome = "Cerveja",
                        Quantidade = 1,
                        ValorUnitario = 5m
                    }
                },
                Pagamentos = new Pagamento[] {
                    new Pagamento {
                        Tipo = "01",
                        Valor = 5
                    }
                },
                Desconto = 0,
            });
            Assert.IsNotNull(response.ChaveAcesso);
        }
        [TestMethod]
        public async Task EmitirBonusAsync_Credencial_Valida_Test()
        {
            var nfeApi = new NfeApi(ValidCredential);
            var response = await nfeApi.EmitirAsync(new Pedido
            {
                Referencia = Guid.NewGuid().ToString(),
                Itens = new Item[] {
                    new Item {
                        Cfop = "5.102",
                        NCM = "2203.00.00",
                        ProdutoId = "1",
                        ProdutoNome = "Cerveja",
                        Quantidade = 1,
                        ValorUnitario = 5m
                    }
                },
                Pagamentos = new Pagamento[0],
                Desconto = 0,
            });
            Assert.IsNotNull(response.ChaveAcesso);
        }

        [TestMethod]
        public async Task CancelarAsyncNaoAutorizado_Test()
        {
            var nfceApi = new NfeApi(ValidCredential);
            var foiCancelado = await nfceApi.CancelarAsync(new Invoices.CancelarNf
            {
                Chave = "31200400000000000000650010000000051842021836",
                Motivo = "Cliente cancelou a compra",
                Referencia = null
            });
            Assert.IsFalse(foiCancelado);
        }

        [TestMethod]
        public async Task DanfeAsyncNaoAutorizado_Test()
        {
            var nfceApi = new NfeApi(ValidCredential);
            var response = await nfceApi.DanfeAsync("31200400000000000000650010000000051842021836");
            Assert.AreEqual("NF-e 1-5 não está autorizada", response.ReasonPhrase);
        }

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password= "123456"
        };       
    }
}
