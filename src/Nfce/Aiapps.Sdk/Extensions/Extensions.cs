using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Aiapps.Sdk
{
    public static class Extensions
    {
        public static StringContent AsJson(this object o)
         => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");

        public static void ConfigAuthorizationBearer(this HttpRequestHeaders header, string token) {
            header.Add("Authorization", $"Bearer {token}");
        }
        public static void AcceptApplicationJson(this HttpRequestHeaders header)
        {
            header.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }        
    }
}
