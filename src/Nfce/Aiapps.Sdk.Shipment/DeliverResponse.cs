using System;

namespace Aiapps.Sdk.Shipment
{
    public class ReserveResponse : Response
    {
        public Operator Operator { get; set; }
        public Device Device { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public DateTime ReservedAt { get; set; }
    }
    public class DeliverResponse : Response
    {
        public Operator DeliveredBy { get; set; }
        public Device DeliveryValidator { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public DateTime DeliveredAt { get; set; }
    }
    public class ReturnedResponse : Response
    {
        public Operator Operator { get; set; }
        public Device Device { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductReference { get; set; }
        public DateTime ReturnedAt { get; set; }
    }
}
