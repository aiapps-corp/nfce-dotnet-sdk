using Aiapps.Sdk.Api;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Hooks.Api
{
    public class HookApi : TokenApi
    {
        private Credential _credential;
        private string route = "api/hooks";

        public HookApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "http://hooks-api.aiapps.com.br";
        }

        public async Task<Hook> Post(Hook value)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  _credential.Token = await Token(_credential.Email, _credential.Password);
              })
              .ExecuteAsync(async () => {
                  var r = await HttpPostAsync(value, route, _credential.Token);
                  return r;
              });

            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Status: {response.StatusCode} - {responseContent}");
            }
            return value;
        }
    }
}
