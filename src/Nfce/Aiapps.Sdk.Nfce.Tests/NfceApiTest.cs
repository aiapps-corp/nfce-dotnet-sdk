using Aiapps.Sdk.Nfce.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Aiapps.Sdk.Orders;
using System.IO;

namespace Aiapps.Sdk.Nfce.Tests
{
    [TestClass]
    public class NfceApiTest
    {
        [TestMethod]
        public async Task EmitirAsync_Credencial_Invalida_Test()
        {
            var nfceApi = new NfceApi(new Credential
            {
                Email = "",
                Password = ""
            });
            var response = await nfceApi.EmitirAsync(new Pedido
            {

            });

            Assert.AreEqual("Credêncial inválida para emissão de nfc-e", response.Erro);
        }

        [TestMethod]
        public async Task EmitirAsync_Credencial_Valida_Conflito_Test()
        {
            var nfceApi = new NfceApi(ValidCredential);
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
            Assert.AreEqual("Pedido 6 já foi enviado", response.Erro);
        }

        [TestMethod]
        public async Task EmitirAsync_Credencial_Valida_Test()
        {
            try
            {
                var nfceApi = new NfceApi(ValidCredential);
                nfceApi.Timeout = TimeSpan.FromSeconds(5);
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
            catch (Exception ex) {
                var text = ex.Message;
            }
        }

        [TestMethod]
        public async Task EmitirAsync2_Credencial_Valida_Test()
        {
            try
            {
                var nfceApi = new NfceApi(ValidCredential);
                nfceApi.Timeout = TimeSpan.FromSeconds(5);
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
            catch (Exception ex)
            {
                var text = ex.Message;
            }
        }

        [TestMethod]
        public void Parse_Test()
        {
            var value = JsonConvert.DeserializeObject<Nfce>(response);
            Assert.AreEqual("Forma de pagamento é necessário.Se não tiver opções cadastre uma forma de pagamento em configurações.", value.Sefaz.Motivo);
        }

        [TestMethod]
        public async Task CancelarAsyncNaoAutorizado_Test()
        {
            var nfceApi = new NfceApi(ValidCredential);
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
            var nfceApi = new NfceApi(ValidCredential);
            var response = await nfceApi.DanfeAsync("21211004228479000179650100000656491004181672");
            var content = await response.Content.ReadAsByteArrayAsync();
            File.WriteAllBytes("danfe.pdf", content);
            Assert.AreEqual("NF-e 1-5 nÃ£o estÃ¡ autorizada", response.ReasonPhrase);
        }

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password = "123456"
        };

        private string response = @"{'numero':null,'serie':null,'chaveAcesso':null,'situacao':null,'cliente':null,'valorTotal':10.0,'itens':[],'url':null,'sefaz':{'emitidoEm':'2020-01-01T00:00:00','protocolo':null,'url':null,'motivo':'Forma de pagamento é necessário.Se não tiver opções cadastre uma forma de pagamento em configurações.','codigoStatus':null}}";
    }
}
