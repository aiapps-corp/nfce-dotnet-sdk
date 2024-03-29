﻿using Aiapps.Sdk.Shipment.Api;
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
            var deliverPackageApi = new DeliverPackageApi(ValidCredential);
            var response = await deliverPackageApi.Deliver(new DeliveredPackage { 
                DeliveredAt = DateTime.UtcNow,
                DeliveredBy = new Operator { Name = "Paulo" },
                DeliveryValidator = new Device { Name = "VALIDATOR-01" },
                OrderReference = "e863d7e1-d05e-4ccc-b75b-2da5438ba278",
                ProductReference = "1",
            });
            Assert.IsNotNull(response.Status);
        }
        [TestMethod]
        public async Task DeliverTrackingNumber_Valid_Credencial_Test()
        {
            var deliverPackageApi = new DeliverPackageApi(ValidCredential);
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

        private static Credential ValidCredential = new Credential
        {
            Email = "teste@aiapps.com.br",
            Password = "123456"
        };
    }
}
