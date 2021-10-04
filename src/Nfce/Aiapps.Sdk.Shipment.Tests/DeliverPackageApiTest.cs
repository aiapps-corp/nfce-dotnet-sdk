using Aiapps.Sdk.Shipment.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Shipment.Tests
{
    [TestClass]
    public class DeliverPackageApiTest
    {
        [TestMethod]
        public async Task Deliver_Valid_Credencial_Test()
        {
            var deliverPackageApi = new DeliverPackageApi(ValidCredencial);
            var response = await deliverPackageApi.Deliver(new DeliveredPackage { 
                DeliveredAt = DateTime.UtcNow,
                DeliveredBy = new Deliveryman { },
                DeliveryValidator = new Device { Name = "VALIDATOR-01" },
                OrderReference = "ac6e86b3-5555-4444-3333-8422f9c77abb",
                ProductReference = "ef7ab2555544443333cf3313637be33",
                TrackingNumber = ""
            });
            Assert.IsNotNull(response.Status);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "",
            Senha = ""
        };
    }
}
