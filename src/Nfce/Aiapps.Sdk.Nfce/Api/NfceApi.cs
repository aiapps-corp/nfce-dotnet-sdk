using Aiapps.Sdk.Api;
using Aiapps.Sdk.Invoices;
using Aiapps.Sdk.Orders;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Nfce.Api
{
    public class NfceApi : TokenApi
    {
        protected string route = "/api/nfce";
        private string _routeCancel = "api/nfce/cancelar";
        private string _routeDanfe = "api/nfce/baixardanfe";
        private string _routeProtocol = "api/nfce/protocol";
        private int _maxRetry = 3;

        private Credential _credential;

        public NfceApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "https://invoices-api.aiapps.com.br";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<Nfce> EmitirAsync(Pedido pedido)
        {
            var nfce = new Nfce();
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
                  .ExecuteAsync(async () =>
                  {
                      return await Policy
                        .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                        .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                        {
                            _credential.Token = await Token(_credential.Email, _credential.Password);
                        })
                        .ExecuteAsync(async () =>
                        {
                            var r = await HttpEmitirAsync(pedido);
                            return r;
                        }).ConfigureAwait(false);
                  }).ConfigureAwait(false);

                responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    nfce = TryParse(nfce, responseContent);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    nfce = TryParse(nfce, responseContent);
                    nfce.Erro = "Módulo bloqueado";
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    nfce = TryParse(nfce, responseContent);
                    nfce.Erro = $"Pedido {pedido.Referencia} já foi enviado";
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    nfce = TryParse(nfce, responseContent);
                    var obj = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    nfce.Erro = $"{obj?.message}";
                }
                else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    nfce = TryParse(nfce, responseContent);
                    nfce.Erro = "Serviço indisponível";
                }
            }
            catch (Exception ex)
            {
                nfce.Erro = $"{responseContent} {ex.Message}";
            }
            return nfce;
        }

        private static Nfce TryParse(Nfce nfce, string responseContent)
        {
            try
            {
                nfce = JsonConvert.DeserializeObject<Nfce>(responseContent);
            }
            catch { }

            return nfce ?? new Nfce();
        }

        public async Task<bool> CancelarAsync(CancelarNf value)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .Handle<Exception>()
              .RetryAsync(_maxRetry, async (exception, retryCount) =>
              {
                  await Task.Delay(300 * retryCount).ConfigureAwait(false);
              })
              .ExecuteAsync(async () =>
              {
                  return await Policy
                    .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                    .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                    {
                        _credential.Token = await Token(_credential.Email, _credential.Password);
                    })
                    .ExecuteAsync(async () =>
                    {
                        var r = await HttpCancelarAsync(value);
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
              .ExecuteAsync(async () =>
              {
                  var r = await HttpDanfeAsync(chaveAcesso);
                  return r;
              });
            return response;
        }
        public async Task<string> GetProtocolAsync(string chaveAcesso)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
              .WaitAndRetryAsync(3, (retry) => TimeSpan.FromSeconds(retry))
              .ExecuteAsync(async () =>
              {
                  var r = await HttpProtocolAsync(chaveAcesso);
                  return r;
              });
            if (response.IsSuccessStatusCode == false)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var protocol = JsonConvert.DeserializeObject<dynamic>(content);
            return protocol.protocol;
        }

        private async Task<HttpResponseMessage> HttpEmitirAsync(Pedido pedido)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = pedido.AsJson();
                var response = await httpClient.PostAsync($"{BaseHttpsAddress}{route}", message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpCancelarAsync(CancelarNf value)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.PostAsync(_routeCancel, value.AsJson());
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

        private async Task<HttpResponseMessage> HttpProtocolAsync(string chaveAcesso)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.GetAsync($"{_routeProtocol}?chaveAcesso={chaveAcesso}");
                return response;
            }
        }
    }
}
