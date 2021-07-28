using Aiapps.Sdk.Hooks;
using Aiapps.Sdk.Hooks.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Licensing.Tests
{
    [TestClass]
    public class HookApiTest
    {
        [TestMethod]
        public async Task Post_Test()
        {
            var hookApi = new HookApi(ValidCredencial);
            var response = await hookApi.Post(new Hook { 
                EventType = EventType.InvoiceAutorized,
                HttpHeaders = "Authorization:7E257336-6AB0-4A22-A650-B455E332392A",
                Url = "https://minhaurl.com.br"
            });

            Assert.IsNotNull(response);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "teste@aiapps.com.br",
            Senha = "123456"
        };
    }
}
