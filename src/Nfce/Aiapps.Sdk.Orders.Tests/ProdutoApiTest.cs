using Aiapps.Sdk.Orders.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Orders.Tests
{
    [TestClass]
    public class ProdutoApiTest
    {
        [TestMethod]
        public async Task Remove_Test()
        {
            var produtoApi = new ProdutoApi(ValidCredential);
            var response = await produtoApi.Remover("2");
            Assert.IsNotNull(response);
        }

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password = "123456"
        };
    }
}
