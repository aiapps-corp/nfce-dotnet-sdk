using System;

namespace Aiapps.Sdk.Hooks
{
    public class Hook
    {
        public EventType EventType { get; set; }
        public string Url { get; set; }
        public string BasicAuthenticationUsername { get; set; }
        public string BasicAuthenticationPassword { get; set; }
        public string HttpHeaders { get; set; }
    }
}
