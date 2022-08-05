using Aiapps.Sdk.Api;
using Aiapps.Sdk.Orders;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Nfe.Api
{
    public class NfeApi : TokenApi
    {
        protected string route = "api/nfe";
        private string _routeCancel = "api/nfe/cancelar";
        private string _routeDanfe = "api/nfe/baixardanfe";
        private int _maxRetry = 3;

        private Credential _credential;

        public NfeApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "https://invoices-api.aiapps.com.br";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<Nfe> EmitirAsync(Pedido pedido)
        {
            var nfe = new Nfe();
            var responseContent = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(_credential.Email) &&
                    string.IsNullOrWhiteSpace(_credential.Password))
                    throw new InvalidOperationException("Credêncial inválida para emissão de nfc-e");

                if (string.IsNullOrWhiteSpace(_credential.Token))
                    _credential.Token = await Token(_credential.Email, _credential.Password);

                var response = await Policy
                  .Handle<Exception>()
                  .RetryAsync(_maxRetry, async (exception, retryCount) =>
                  {
                      await Task.Delay(300 * retryCount).ConfigureAwait(false);
                  })
                  .ExecuteAsync(async () => {
                      return await Policy
                        .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                        .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                        {
                            _credential.Token = await Token(_credential.Email, _credential.Password);
                        })
                        .ExecuteAsync(async () => {
                            var r = await HttpEmitirAsync(pedido);
                            return r;
                        }).ConfigureAwait(false);
                  }).ConfigureAwait(false);

                responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    nfe = TryParse(nfe, responseContent);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    nfe = TryParse(nfe, responseContent);
                    nfe.Erro = "Módulo bloqueado";
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    nfe = TryParse(nfe, responseContent);
                    nfe.Erro = $"Pedido {pedido.Referencia} já foi enviado";
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest && string.IsNullOrWhiteSpace(nfe.Sefaz.Motivo))
                {
                    nfe = TryParse(nfe, responseContent);
                    var obj = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    nfe.Erro = $"{obj?.message}";
                }
                else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    nfe = TryParse(nfe, responseContent);
                    nfe.Erro = "Serviço indisponível";
                }
            }
            catch (Exception ex)
            {
                nfe.Erro = $"{responseContent} {ex.Message}";
            }
            return nfe;
        }

        private static Nfe TryParse(Nfe nce, string responseContent)
        {
            try
            {
                nce = JsonConvert.DeserializeObject<Nfe>(responseContent);
            }
            catch { }

            return nce ?? new Nfe();
        }

        public async Task<bool> CancelarAsync(string chaveAcesso, string motivo, string referencia)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .Handle<Exception>()
              .RetryAsync(_maxRetry, async (exception, retryCount) =>
              {
                  await Task.Delay(300 * retryCount).ConfigureAwait(false);
              })
              .ExecuteAsync(async () => {
                  return await Policy
                    .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                    .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                    {
                        _credential.Token = await Token(_credential.Email, _credential.Password);
                    })
                    .ExecuteAsync(async () => {
                        var r = await HttpCancelarAsync(chaveAcesso, motivo, referencia);
                        return r;
                    }).ConfigureAwait(false);
              }).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> DanfeAsync(string chaveAcesso)
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
                  var r = await HttpDanfeAsync(chaveAcesso);
                  return r;
              });
            return response;
        }

        private async Task<HttpResponseMessage> HttpEmitirAsync(Pedido pedido)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = pedido.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpCancelarAsync(string chaveAcesso, string motivo, string referencia)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.PostAsync(_routeCancel, new { chave = chaveAcesso, motivo, referencia }.AsJson());
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpDanfeAsync(string chaveAcesso)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.GetAsync($"{_routeDanfe}?chaveAcesso={chaveAcesso}");
                return response;
            }
        }
    }
}
