﻿using Aiapps.Sdk.Api;
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
        private string _routeXml = "api/nfce/baixarxml";
        private string _routeProtocol = "api/nfce/protocol";
        private string _routeNotifyThirdParty = "api/nfce/notifyThirdParty";

        private Credential _credential;

        public NfceApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "https://invoices-api.aiapps.com.br";
            //BaseHttpsAddress = "http://localhost:50188";
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
                  .WaitAndRetryAsync(MaxRetry, (retry) => TimeSpan.FromSeconds(retry))
                  .ExecuteAsync(async () =>
                  {
                      return await Policy
                        .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                        .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                        {
                            await RetryToken(_credential);
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
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    nfce = TryParse(nfce, responseContent);
                    nfce.Erro = $"{responseContent}";
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
              .WaitAndRetryAsync(MaxRetry, (retry) => TimeSpan.FromSeconds(retry))
              .ExecuteAsync(async () =>
              {
                  return await Policy
                    .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                    .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                    {
                        await RetryToken(_credential);
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
                  await RetryToken(_credential);
              })
              .ExecuteAsync(async () =>
              {
                  var r = await HttpDanfeAsync(chaveAcesso);
                  return r;
              });
            return response;
        }

        public async Task<HttpResponseMessage> XmlAsync(string chaveAcesso)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
              .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
              {
                  await RetryToken(_credential);
              })
              .ExecuteAsync(async () =>
              {
                  var r = await HttpXmlAsync(chaveAcesso);
                  return r;
              });
            return response;
        }
        public async Task<InvoiceInfo> GetProtocolAsync(string chaveAcesso)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound || r.StatusCode == HttpStatusCode.Accepted)
              .WaitAndRetryAsync(MaxRetry, (retry) => TimeSpan.FromSeconds(retry))
              .ExecuteAsync(async () =>
              {
                  var r = await HttpProtocolAsync(chaveAcesso);
                  return r;
              });
            if (response.IsSuccessStatusCode == false)
                return null;

            if(response.StatusCode == HttpStatusCode.NotFound)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            var protocol = JsonConvert.DeserializeObject<InvoiceInfo>(content);
            return protocol;
        }
        public async Task NotifyThirdParty(NotifyThirdPartyNfResult value)
        {
            if (string.IsNullOrWhiteSpace(_credential.Token))
                _credential.Token = await Token(_credential.Email, _credential.Password);

            var response = await Policy
              .Handle<Exception>()
              .WaitAndRetryAsync(MaxRetry, (retry) => TimeSpan.FromSeconds(retry))
              .ExecuteAsync(async () =>
              {
                  return await Policy
                    .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                    .RetryAsync(1, onRetryAsync: async (exception, retryCount) =>
                    {
                        await RetryToken(_credential);
                    })
                    .ExecuteAsync(async () =>
                    {
                        var r = await HttpNotifyThirdPartyAsync(value);
                        return r;
                    }).ConfigureAwait(false);
              }).ConfigureAwait(false);
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

        private async Task<HttpResponseMessage> HttpXmlAsync(string chaveAcesso)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.GetAsync($"{_routeXml}?chaveAcesso={chaveAcesso}");
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

        private async Task<HttpResponseMessage> HttpNotifyThirdPartyAsync(NotifyThirdPartyNfResult value)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var response = await httpClient.PostAsync(_routeNotifyThirdParty, value.AsJson());
                return response;
            }
        }
    }
}
