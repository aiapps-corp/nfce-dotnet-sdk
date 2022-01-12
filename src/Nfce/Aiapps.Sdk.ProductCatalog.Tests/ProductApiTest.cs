using Aiapps.Sdk.ProductCatalog.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Aiapps.Sdk.ProductCatalog.Tests
{
    [TestClass]
    public class ProductApiTest
    {
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
