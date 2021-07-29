using Aiapps.Sdk.Orders;
using Aiapps.Sdk.Orders.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Nfce.Tests
{
    [TestClass]
    public class ProdutoApiTest
    {
        [TestMethod]
        public async Task CadastrarOuAtualizarAsync_Test()
        {
            var produtoApi = new ProdutoApi(ValidCredencial);
            var retorno = await produtoApi.CadastrarOuAtualizarAsync(new Produto
            {
                Id = "1",
                Nome = "Cerveja 1",
                Cfop = "5.102",
                Ncm = "2203.00.00",
                Valor = 5m
            });
            Assert.IsTrue(retorno.Sucesso);
        }

        [TestMethod]
        public async Task CadastrarAsync_Test()
        {
            var produtoApi = new ProdutoApi(ValidCredencial);
            var retorno = await produtoApi.CadastrarOuAtualizarAsync(new Produto
            {
                Id = "2",
                Nome = "Cerveja 2",
                Cfop = "5.102",
                Ncm = "2203.00.00",
                Valor = 5m
            });
            Assert.IsTrue(retorno.Sucesso);
        }

        [TestMethod]
        public async Task RemoverAsync_Test()
        {
            var produtoApi = new ProdutoApi(ValidCredencial);
            var retorno = await produtoApi.Remover("2");
            Assert.IsTrue(retorno.Sucesso);
        }

        [TestMethod]
        public async Task EntregarAsync_Test()
        {
            var produtoApi = new ProdutoApi(ValidCredencial);
            var retorno = await produtoApi.Entregar(new Entrega { 
                ProdutoId = "1",
                ReferenciaPedido = "77487fac-324d-4a8e-9fc0-14f06f7fc482"
            });
            Assert.AreEqual(
                @"Falha ao entregar produto (Emissão de NFC-e) {""message"":""Não tem mais estoque reservado para o pedido: 7, produto Cerveja 1""}",
                retorno.Mensagem);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "teste@aiapps.com.br",
            Senha = "123456"
        };
    }
}
