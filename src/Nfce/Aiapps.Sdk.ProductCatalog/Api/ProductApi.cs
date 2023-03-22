using Aiapps.Sdk.Api;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.ProductCatalog.Api
{
    public class ProductApi : TokenApi
    {
        private Credential _credential;

        public ProductApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "https://production-api.aiapps.com.br";
        }

        public async Task<Response> AddOrUpdate(Product product)
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
                  var r = await HttpUpdateAsync(product);
                  return r;
              });

            if (response.StatusCode == HttpStatusCode.NotFound)
                response = await HttpAddAsync(product);

            var responseContent = await response.Content.ReadAsStringAsync();
            var retorno = new Response { 
                IsSuccessStatus = true,
                Data = TryParseProduct(responseContent),
            };
            if (response.IsSuccessStatusCode == false)
            {
                retorno.IsSuccessStatus = false;
                retorno.StatusMessage = $"Falha ao sincronizar produto ({response.StatusCode}) - ({responseContent})";                
            }
            return retorno;
        }

        private Product TryParseProduct(string responseContent)
        {
            try
            {
                return JsonConvert.DeserializeObject<Product>(responseContent);
            }
            catch {
                return null;
            }
        }

        public async Task<Response> Remove(string id)
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
                  var r = await HttpRemoveAsync(id);
                  return r;
              });
            var retorno = new Response { IsSuccessStatus = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.IsSuccessStatus = false;
                retorno.StatusMessage = $"Falha ao remover produto (Emissão de NFC-e) {responseContent}";
            }
            return retorno;
        }

        private async Task<HttpResponseMessage> HttpAddAsync(Product product)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var url = $"api/productcatalog";
                var message = product.AsJson();
                var response = await httpClient.PostAsync(url, message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpUpdateAsync(Product product)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var url = $"api/productcatalog/{(product.Id)}";
                var message = product.AsJson();
                var response = await httpClient.PutAsync(url, message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpRemoveAsync(string id)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var url = $"api/productcatalog/{id.Replace("-", "")}";
                var response = await httpClient.DeleteAsync(url);
                return response;
            }
        }
    }
}
