﻿using System;
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
        public string DisplayName { get; set; }
    }

    public class ActivateLicenseRequest : LicenseRequest
    {
    }

    public class CancelLicenseRequest : LicenseRequest
    {
    }
}
