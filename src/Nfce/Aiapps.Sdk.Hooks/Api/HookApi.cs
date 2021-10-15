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
        private Credencial _credencial;
        private string route = "api/hooks";

        public HookApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
            BaseHttpsAddress = "http://hooks-api.aiapps.com.br";
        }

        public async Task<Hook> Post(Hook value)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
              })
              .ExecuteAsync(async () => {
                  var r = await HttpPostAsync(value, route, _credencial.Token);
                  return r;
              });

            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception(responseContent);
            }
            return value;
        }
    }
}
