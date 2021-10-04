using System;

namespace Aiapps.Sdk.Shipment
{
    public class DeliverResponse
    {
        public Deliveryman DeliveredBy { get; set; }
        public Device DeliveryValidator { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}
