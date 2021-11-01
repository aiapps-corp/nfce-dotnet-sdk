using Aiapps.Sdk;
using Aiapps.Sdk.Api;
using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Pos.Api
{
    public class MovimentacaoApi : TokenApi
    {
        private Credencial _credencial;
        private string route = "api/pos/registrar";

        public MovimentacaoApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
            BaseHttpsAddress = "https://sales-api.aiapps.com.br";
        }

        public async Task<Retorno> Registrar(Movimentacao movimentacao)
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
                  var r = await HttpRegistrarAsync(movimentacao);
                  return r;
              });
            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao registrar movimentação na POS {responseContent}";
            }
            return retorno;
        }

        private async Task<HttpResponseMessage> HttpRegistrarAsync(Movimentacao movimentacao)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = movimentacao.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }
    }
}
