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

        [TestMethod]
        public async Task ProtocolAsyncNaoAutorizado_Test()
        {
            var nfceApi = new NfceApi(ValidCredential);
            var protocol = await nfceApi.GetProtocolAsync("21211004228479000179650100000656491004181672");
            Assert.IsNull(protocol);
        }
        [TestMethod]
        public async Task EmitirAsync_Test()
        {
            var nfceApi = new NfceApi(new Credential
            {
                Email = "integracao2@cantopraia.com.br",
                Password = "123123"
            });
            var json = @"{""Numero"":null,""Serie"":null,""DataHora"":""2023-12-28T02:49:38.69"",""Cfop"":""5.405"",""Cliente"":{""Documento"":""03072867038"",""Nome"":""EDUARDA COSTA DA ROSA"",""Email"":null,""Telefone"":null,""Sexo"":2,""DataNascimento"":""1998-10-28T00:00:00"",""Endereco"":{""Cep"":null,""Logradouro"":null,""Numero"":null,""Complemento"":null,""Bairro"":null},""Conta"":null,""TagConta"":null,""Titular"":null},""Canal"":{""Codigo"":null,""Nome"":null,""IconeUrl"":null},""Vendedor"":{""Codigo"":null,""Documento"":"""",""Nome"":null},""PontoVenda"":{""Codigo"":""6d6f4b21-e790-44e2-a5fc-9e6d360dcf7f"",""MacAddress"":""357400572684158"",""Nome"":""PAG16577""},""Itens"":[{""NCM"":""22029900"",""Cfop"":null,""EhTaxa"":false,""Customizacao"":[],""ProdutoId"":""98df9fbbbda344ffbd94bb00ccbbe1de"",""ProdutoNome"":""RED BULL TRADICIONAL"",""Quantidade"":1.00000,""QuantidadeMovimentacaoEstoque"":null,""ValorUnitario"":16.15,""Desconto"":0.00,""Uuid"":null}],""Pagamentos"":[{""DataVencimento"":null,""Tipo"":""05"",""Descricao"":""Cashless"",""Valor"":16.1500000,""Cartao"":{""Cnpj"":null,""Bandeira"":null,""Nsu"":null},""Referencia"":null}],""Entrega"":{""Frete"":0.0,""TipoFrete"":9,""Transportador"":{""Codigo"":null,""Documento"":null,""Nome"":null},""Endereco"":{""Cep"":null,""Logradouro"":null,""Numero"":null,""Complemento"":null,""Bairro"":null}},""IndicadorIntermediador"":null,""IntermediadorTransacao"":{""Cnpj"":null,""IdentificadorIntermediador"":null},""Desconto"":0.0,""ContaCliente"":null,""Referencia"":""9e55c31f-35ec-4690-8189-1888d8a081fd"",""Situacao"":null,""Assincrono"":true}";
            var pedido = JsonConvert.DeserializeObject<Pedido>(json);
            var response = await nfceApi.EmitirAsync(pedido);

            Assert.AreEqual("Credêncial inválida para emissão de nfc-e", response.Erro);
        }

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password = "123456"
        };

        private string response = @"{'numero':null,'serie':null,'chaveAcesso':null,'situacao':null,'cliente':null,'valorTotal':10.0,'itens':[],'url':null,'sefaz':{'emitidoEm':'2020-01-01T00:00:00','protocolo':null,'url':null,'motivo':'Forma de pagamento é necessário.Se não tiver opções cadastre uma forma de pagamento em configurações.','codigoStatus':null}}";
    }
}
