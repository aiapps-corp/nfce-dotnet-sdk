using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Licensing
{
    public class LicenseRequest
    {
        public string Id { get; set; }
    }

    public class ConnectLicenseRequest : LicenseRequest
    {
    }

    public class ActivateLicenseRequest : LicenseRequest
    {
    }

    public class CancelLicenseRequest : LicenseRequest
    {
    }
}
