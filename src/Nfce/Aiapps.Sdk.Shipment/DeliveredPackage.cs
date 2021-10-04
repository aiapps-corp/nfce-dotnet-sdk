using Aiapps.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Shipment
{
    public class DeliveredPackage
    {
        public Deliveryman DeliveredBy { get; set; }
        public Device DeliveryValidator { get; set; }
        public string OrderReference { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}
