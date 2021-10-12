using System;

namespace Aiapps.Sdk.Shipment
{
    public class DeliverResponse : Response
    {
        public Deliveryman DeliveredBy { get; set; }
        public Device DeliveryValidator { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
}
