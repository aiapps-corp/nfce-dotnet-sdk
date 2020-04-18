using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aiapps.Nfce.Api
{
    public abstract class TokenApi
    {
        public string BaseHttpsAddress { get; set; } = "https://mobi.aiapps.com.br";
        protected static HttpClientHandler clientHandler = new HttpClientHandler();
        protected async Task<string> Token(string username, string password)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var content = new StringContent($"grant_type=password&username={username}&password={password}");
                var response = await httpClient.PostAsync("token", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                dynamic jsonObj = JsonConvert.DeserializeObject(responseContent);
                var expires_in = (double)jsonObj.expires_in;
                var token = jsonObj.access_token.ToString();
                var expiryDate = DateTime.UtcNow.AddSeconds(expires_in);
                return token;
            }
        }
    }
}
