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
                DeliveredBy = new Operator { },
                DeliveryValidator = new Device { Name = "VALIDATOR-01" },
                OrderReference = "30ad0ba9-ac93-4640-8bae-b4bf363bf2ab",
                ProductReference = "b87935a0e3da41f28808af0210e22712",
            });
            Assert.IsNotNull(response.Status);
        }
        [TestMethod]
        public async Task DeliverTrackingNumber_Valid_Credencial_Test()
        {
            var deliverPackageApi = new DeliverPackageApi(ValidCredencial);
            var response = await deliverPackageApi.Deliver(new DeliveredPackage
            {
                Reference = "30ad0ba9-ac93-4640-8bae-b4bf363bf2ab",
                DeliveredAt = DateTime.UtcNow,
                DeliveredBy = new Operator { },
                DeliveryValidator = new Device { Name = "VALIDATOR-01" },
                ProductReference = "b87935a0e3da41f28808af0210e22712",
                TrackingNumber = "30ad0ba9-ac93-4640-8bae-b4bf363bf2ab"
            });
            Assert.IsNotNull(response.Status);
        }

        private static Credencial ValidCredencial = new Credencial
        {
            Email = "integracao-2@meep.cloud",
            Senha = "123456"
        };
    }
}
