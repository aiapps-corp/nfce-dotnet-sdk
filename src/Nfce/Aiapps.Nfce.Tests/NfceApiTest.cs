using Aiapps.Nfce.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual("Credêncial inválida para emissão de nfc-e", response.Erro);
        }

        [TestMethod]
        public async Task EmitirAsync_RequisicaoInvalida_Test()
        {
            var nfceApi = new NfceApi(new Credencial
            {
                Email = "teste@aiapps.com.br",
                Senha = "123456"
            });
            var response = await nfceApi.EmitirAsync(new Pedido
            {
                Referencia = "1",
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
            Assert.AreEqual("Requisição inválida", response.Erro);
        }
    }
}
