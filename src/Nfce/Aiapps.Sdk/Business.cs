using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk
{
    public class Business
    {
        public string Document { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string CellPhone { get; set; }
        public string CommercialPhone { get; set; }
        public string StateRegistration { get; set; }
        public string MunicipalRegistration { get; set; }
        public bool IsOptingForSimple { get; set; }

        public string CouponCode { get; set; }
    }
}
