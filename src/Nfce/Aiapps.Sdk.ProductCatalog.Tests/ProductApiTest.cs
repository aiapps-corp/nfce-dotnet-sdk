using Aiapps.Sdk.ProductCatalog.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Aiapps.Sdk.ProductCatalog.Tests
{
    [TestClass]
    public class ProductApiTest
    {
        [TestMethod]
        public async Task CadastrarOuAtualizarAsync_Test()
        {
            var produtoApi = new ProductApi(ValidCredential);
            var retorno = await produtoApi.AddOrUpdate(new Product
            {
                Id = "1",
                Name = "Cerveja 1",
                Cfop = "5.102",
                Ncm = "2203.00.00",
                SaleValue = 5m
            });
            Assert.IsTrue(retorno.IsSuccessStatus);
        }

        [TestMethod]
        public async Task CadastrarAsync_Test()
        {
            var produtoApi = new ProductApi(ValidCredential);
            var retorno = await produtoApi.AddOrUpdate(new Product
            {
                Id = "2",
                Name = "Cerveja 2",
                Cfop = "5.102",
                Ncm = "2203.00.00",
                SaleValue = 5m
            });
            Assert.IsTrue(retorno.IsSuccessStatus);
        }

        //[TestMethod]
        //public async Task EntregarAsync_Test()
        //{
        //    var produtoApi = new ProductApi(ValidCredential);
        //    var retorno = await produtoApi.Entregar(new Entrega
        //    {
        //        ProdutoId = "1",
        //        ReferenciaPedido = "77487fac-324d-4a8e-9fc0-14f06f7fc482"
        //    });
        //    Assert.AreEqual(
        //        @"Falha ao entregar produto (Emissão de NFC-e) {""message"":""Não tem mais estoque reservado para o pedido: 7, produto Cerveja 1""}",
        //        retorno.Mensagem);
        //}

        [TestMethod]
        public async Task Remove_Test()
        {
            var productApi = new ProductApi(ValidCredential);
            var response = await productApi.Remove("2");
            Assert.IsNotNull(response.Status);
        }

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password = "123456"
        };
    }
}
