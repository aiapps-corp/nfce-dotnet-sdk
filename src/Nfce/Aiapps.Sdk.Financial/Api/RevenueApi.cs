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

        private Credencial _credencial;

        public RevenueApi(Credencial credencial)
        {
            //BaseHttpsAddress = "https://localhost:49153";
            BaseHttpsAddress = "https://financial-api.aiapps.com.br";
            _credencial = credencial ?? new Credencial();
        }

        public async Task<Retorno> Post(Revenue value)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
              })
              .ExecuteAsync(async () =>
              {
                  var r = await HttpPostAsync(value, _revenueRoute, _credencial.Token);
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
    }

    [Obsolete]
    public class ReceitaApi : TokenApi
    {
        private string _revenueRoute = "api/receitas";

        private Credencial _credencial;

        public ReceitaApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
        }

        public async Task<Retorno> Post(Receita value)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
              })
              .ExecuteAsync(async () =>
              {
                  var r = await HttpPostAsync(value, _revenueRoute, _credencial.Token);
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
    }
}
