using Aiapps.Sdk.Api;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Shipment.Api
{
    public class DeliverPackageApi : TokenApi
    {
        protected string ShipmentBaseHttpsAddress { get; set; } = "https://inventory-api.aiapps.com.br";
        private string _routeDeliver = "api/shipment/deliver";

        private Credencial _credencial;

        public DeliverPackageApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
        }

        public async Task<DeliverResponse> Deliver(DeliveredPackage deliveredPackage)
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
                  var r = await HttpDeliverAsync(deliveredPackage);
                  return r;
              });
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<DeliverResponse>(responseContent);
            return responseObject;
        }

        private async Task<HttpResponseMessage> HttpDeliverAsync(DeliveredPackage deliveredPackage)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(ShipmentBaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

                var message = deliveredPackage.AsJson();
                var response = await httpClient.PostAsync(_routeDeliver, message);
                return response;
            }
        }
    }
}
