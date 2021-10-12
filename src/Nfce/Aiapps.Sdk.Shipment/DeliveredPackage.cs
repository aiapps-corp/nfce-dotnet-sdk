using Aiapps.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Shipment
{
    public class DeliveredPackage
    {
        public string Reference { get; set; }

        public Deliveryman DeliveredBy { get; set; } = new Deliveryman();
        public Device DeliveryValidator { get; set; } = new Device();
        public string OrderReference { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public decimal QuantityDelivered { get; set; } = 1;
        public decimal? UnitValue { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
    public class ChangeTrackingNumberRequest
    {
        public string Reference { get; set; }

        public string OldTrackingNumber { get; set; }
        public string NewTrackingNumber { get; set; }
    }
}
