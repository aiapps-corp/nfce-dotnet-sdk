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
            var nfceApi = new NfceApi(new Credencial { 
                Email = "",
                Senha = ""
            });
            var response = await nfceApi.EmitirAsync(new Nfce { 
            
            });

            Assert.AreEqual("Credêncial inválida para emissão de nfc-e", response.Erro);
        }

        [TestMethod]
        public async Task EmitirAsync_Test()
        {
            var nfceApi = new NfceApi(new Credencial
            {
                Email = "teste@aiapps.com.br",
                Senha = "123456"
            });
            var response = await nfceApi.EmitirAsync(new Nfce
            {

            });
        }
    }
}
