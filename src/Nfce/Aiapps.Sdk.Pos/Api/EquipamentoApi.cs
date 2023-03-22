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
    public class EquipamentoApi : TokenApi
    {
        private Credential _credential;
        private string route = "api/equipamentos";

        public EquipamentoApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "https://sales-api.aiapps.com.br";
        }

        public async Task<Retorno> Registrar(Equipamento equipamento)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  await RetryToken(_credential);
              })
              .ExecuteAsync(async () => {
                  var r = await HttpRegistrarAsync(equipamento);
                  return r;
              });
            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao registrar equipamento {responseContent}";
            }
            return retorno;
        }

        private async Task<HttpResponseMessage> HttpRegistrarAsync(Equipamento equipamento)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = equipamento.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }
    }
}
