using Aiapps.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Shipment
{
    public class Package
    {
        public string Reference { get; set; }

        public string OrderReference { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public decimal Quantity { get; set; } = 1;
        public decimal? UnitValue { get; set; }
    }
    public class ReservedPackage : Package
    {
        public Operator Operator { get; set; } = new Operator();
        public Device Device { get; set; } = new Device();
        public DateTime ReservedAt { get; set; }
    }
    public class DeliveredPackage : Package
    {
        public Operator DeliveredBy { get; set; } = new Operator();
        public Device DeliveryValidator { get; set; } = new Device();
        public DateTime DeliveredAt { get; set; }
    }
    public class ReturnedPackage : Package
    {
        public Operator Operator { get; set; } = new Operator();
        public Device Device { get; set; } = new Device();
        public DateTime ReturnedAt { get; set; }
    }

    public class ChangeTrackingNumberRequest
    {
        public string Reference { get; set; }

        public string OldTrackingNumber { get; set; }
        public string NewTrackingNumber { get; set; }
    }
}
