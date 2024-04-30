using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;

namespace Aiapps.Sdk.Api
{
    public abstract class TokenApi
    {
        private int _maxRetry = 1;
#if DEBUG
        protected string BaseAuthHttpsAddress { get; set; } = "https://auth.aiapps.com.br";
#else
        protected string BaseAuthHttpsAddress { get; set; } = "https://auth.aiapps.com.br";
#endif
#if DEBUG
        protected string BaseHttpsAddress { get; set; } = "https://api.aiapps.com.br";
#else
        protected string BaseHttpsAddress { get; set; } = "https://api.aiapps.com.br";
#endif

        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(59);

        public int MaxRetry { get => _maxRetry; set => _maxRetry = value; }

        protected static HttpClientHandler clientHandler = new HttpClientHandler();
        protected async Task<string> Token(string username, string password)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseAuthHttpsAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                httpClient.Timeout = Timeout;
                var usernameEncoded = HttpUtility.UrlEncode(username);
                var passwordEncoded = HttpUtility.UrlEncode(password);
                var content = new StringContent($"grant_type=password&username={usernameEncoded}&password={passwordEncoded}");
                var response = await httpClient.PostAsync("token", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic jsonObj = JsonConvert.DeserializeObject(responseContent);
                if (!response.IsSuccessStatusCode)
                {
                    string error = jsonObj.error_description.ToString();
                    throw new ApplicationException(error);
                }
                var expires_in = (double)jsonObj.expires_in;
                var token = jsonObj.access_token.ToString();
                var expiryDate = DateTime.UtcNow.AddSeconds(expires_in);
                return token;
            }
        }

        protected async Task RetryToken(Credential credential)
        {
            try
            {
                credential.Token = await Token(credential.Email, credential.Password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected async Task<HttpResponseMessage> HttpPostAsync<T>(T value, string route, string token)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = value.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }

        protected async Task<HttpResponseMessage> HttpDeleteAsync(string id, string route, string token)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.DeleteAsync($"{BaseHttpsAddress}/{route}/{id}");
                return response;
            }
        }
    }
}
