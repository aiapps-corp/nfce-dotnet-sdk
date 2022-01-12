﻿using Aiapps.Sdk.Api;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Orders.Api
{
    public class ProdutoApi : TokenApi
    {
        private string _routeEntregar = "api/produtos/entregar";

        private Credential _credential;

        public ProdutoApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "https://production-api.aiapps.com.br";
        }

        public async Task<Retorno> CadastrarOuAtualizarAsync(Produto produto)
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
                  var r = await HttpAtualizarAsync(produto);
                  return r;
              });

            if (response.StatusCode == HttpStatusCode.NotFound)
                response = await HttpCadastrarAsync(produto);

            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao sincronizar produto (Emissão de NFC-e) {responseContent}";                
            }
            return retorno;
        }

        public async Task<Retorno> Remover(string id)
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
                  var r = await HttpRemoverAsync(id);
                  return r;
              });
            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao remover produto (Emissão de NFC-e) {responseContent}";
            }
            return retorno;
        }

        public async Task<Retorno> Entregar(Entrega entrega)
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
                  var r = await HttpEntregarAsync(entrega);
                  return r;
              });
            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao entregar produto (Emissão de NFC-e) {responseContent}";
            }
            return retorno;
        }

        private async Task<HttpResponseMessage> HttpCadastrarAsync(Produto produto)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var url = $"api/produtos";
                var message = produto.AsJson();
                var response = await httpClient.PostAsync(url, message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpAtualizarAsync(Produto produto)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var url = $"api/produtos?id={(produto.Id)}";
                var message = produto.AsJson();
                var response = await httpClient.PutAsync(url, message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpRemoverAsync(string id)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var url = $"api/produtos?id={id.Replace("-", "")}";
                var response = await httpClient.DeleteAsync(url);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpEntregarAsync(Entrega entrega)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = entrega.AsJson();
                var response = await httpClient.PostAsync(_routeEntregar, message);
                return response;
            }
        }
    }
}
