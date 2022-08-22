using Aiapps.Sdk.Licensing.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Licensing.Tests
{
    [TestClass]
    public class LicenseApiTest
    {
        [TestMethod]
        public async Task Connect_Test()
        {
            var licenseApi = new LicenseApi(ValidCredential);
            var response = await licenseApi.Connect(new ConnectLicenseRequest { 
                Id = "BE066D39-2B67-76C7-41C6-52357E7E3651",
            });
        }
        [TestMethod]
        public async Task Cancel_Test()
        {
            var licenseApi = new LicenseApi(ValidCredential);
            await licenseApi.Cancel(new CancelLicenseRequest
            {
                Id = "BE066D39-2B67-76C7-41C6-52357E7E3651",
            });
        }
        [TestMethod]
        public async Task Activate_Test()
        {
            var licenseApi = new LicenseApi(ValidCredential);
            await licenseApi.Activate(new ActivateLicenseRequest
            {
                Id = "BE066D39-2B67-76C7-41C6-52357E7E3651",
            });
        }

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password= "123456"
        };
    }
}
