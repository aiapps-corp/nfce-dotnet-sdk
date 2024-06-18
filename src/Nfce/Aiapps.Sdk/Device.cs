using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk
{
    public class Device
    {
        /// <summary>
        /// Identificador do pagamento
        /// </summary>
        public string Reference { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Identificador do terminal de pagamento
        /// </summary>
        public string PaymentDeviceId { get; set; }
    }
}
