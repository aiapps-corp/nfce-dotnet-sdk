using Aiapps.Nfce.Api;
using Aiapps.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Aiapps.Nfce.Tests
{
    [TestClass]
    public class NfceApiTest
    {
        [TestMethod]
        public async Task EmitirAsync_Credencial_Invalida_Test()
        {
            var nfceApi = new NfceApi(new Credencial
            {
                Email = "",
                Senha = ""
            });
            var response = await nfceApi.EmitirAsync(new Pedido
            {

            });

            Assert.AreEqual("Cred�ncial inv�lida para emiss�o de nfc-e", response.Erro);
        }

        [TestMethod]
        public async Task EmitirAsync_Credencial_Valida_Conflito_Test()
        {
            var nfceApi = new NfceApi(ValidCredencial);
            var response = await nfceApi.EmitirAsync(new Pedido
            {
                Referencia = "6",
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
            Assert.AreEqual("Pedido 6 j� foi enviado", response.Erro);
        }

        [TestMethod]
        public async Task EmitirAsync_Credencial_Valida_Test()
        {
            var nfceApi = new NfceApi(ValidCredencial);
            var response = await nfceApi.EmitirAsync(new Pedido
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
        public void Parse_Test()
        {
            var value = JsonConvert.DeserializeObject<Nfce>(response);
            Assert.AreEqual("Forma de pagamento � necess�rio.Se n�o tiver op��es cadastre uma forma de pagamento em configura��es.", value.Sefaz.Motivo);
        }

        [TestMethod]
        public async Task CancelarAsyncNaoAutorizado_Test()
        {
            var nfceApi = new NfceApi(ValidCredencial);
            var foiCancelado = await nfceApi.CancelarAsync("31200400000000000000650010000000051842021836", "Cliente cancelou a compra");
            Assert.IsFalse(foiCancelado);
        }

        [TestMethod]
        public async Task DanfeAsyncNaoAutorizado_Test()
        {
            var nfceApi = new NfceApi(ValidCredencial);
            var response = await nfceApi.DanfeAsync("31200400000000000000650010000000051842021836");
            Assert.AreEqual("NF-e 1-5 não está autorizada", response.ReasonPhrase);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "teste@aiapps.com.br",
            Senha = "123456"
        };

        private string response = @"{'numero':null,'serie':null,'chaveAcesso':null,'situacao':null,'cliente':null,'valorTotal':10.0,'itens':[],'url':null,'sefaz':{'emitidoEm':'2020-01-01T00:00:00','protocolo':null,'url':null,'motivo':'Forma de pagamento � necess�rio.Se n�o tiver op��es cadastre uma forma de pagamento em configura��es.','codigoStatus':null}}";
    }
}
