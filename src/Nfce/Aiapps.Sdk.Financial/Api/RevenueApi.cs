using Aiapps.Sdk.Api;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Financial.Api
{
    public class RevenueApi : TokenApi
    {
        private string _revenueRoute = "api/revenues";

        private Credential _credential;

        public RevenueApi(Credential credential)
        {
            BaseHttpsAddress = "https://financial-api.aiapps.com.br";
            _credential = credential ?? new Credential();
        }

        public async Task<Retorno> Post(Revenue value)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  _credential.Token = await Token(_credential.Email, _credential.Password);
              })
              .ExecuteAsync(async () =>
              {
                  var r = await HttpPostAsync(value, _revenueRoute, _credential.Token);
                  return r;
              });

            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao sincronizar receita {responseContent}";
            }
            return retorno;
        }

        public async Task<Retorno> Delete(string reference)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  _credential.Token = await Token(_credential.Email, _credential.Password);
              })
              .ExecuteAsync(async () =>
              {
                  var r = await HttpDeleteAsync(reference, _revenueRoute, _credential.Token);
                  return r;
              });

            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao remover receita {responseContent}";
            }
            return retorno;
        }
    }
}
