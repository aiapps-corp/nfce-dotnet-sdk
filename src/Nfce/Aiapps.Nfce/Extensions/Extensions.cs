using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Aiapps.Nfce
{
    public static class Extensions
    {
        public static StringContent AsJson(this object o)
         => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }
}
