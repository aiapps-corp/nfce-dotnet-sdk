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
        private string _routeReserve = "api/shipment/reserve";
        private string _routeDeliver = "api/shipment/deliver";
        private string _routeReturn = "api/shipment/return";
        private string _routeChangeTrackingNumber = "api/shipment/changeTrackingNumber";

        private Credential _credential;

        public DeliverPackageApi(Credential credencial)
        {
            _credential = credencial ?? new Credential();
        }

        public async Task<ReserveResponse> Reserve(ReservedPackage package)
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
                  var r = await HttpPostAsync(package, _routeReserve);
                  return r;
              });
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                var responseObject = JsonConvert.DeserializeObject<ReserveResponse>(responseContent);
                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception(responseContent, ex);
            }
        }

        public async Task<DeliverResponse> Deliver(DeliveredPackage deliveredPackage)
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
                  var r = await HttpPostAsync(deliveredPackage, _routeDeliver);
                  return r;
              });
            var responseContent = await response.Content.ReadAsStringAsync();
            return TryDeserializeObject<DeliverResponse>(responseContent);
        }

        public async Task<ReturnedResponse> Return(ReturnedPackage deliveredPackage)
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
                  var r = await HttpPostAsync(deliveredPackage, _routeReturn);
                  return r;
              });
            var responseContent = await response.Content.ReadAsStringAsync();
            return TryDeserializeObject<ReturnedResponse>(responseContent);
        }

        public async Task<Response> ChangeTrackingNumber(ChangeTrackingNumberRequest value)
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
                  var r = await HttpPostAsync(value, _routeChangeTrackingNumber);
                  return r;
              });
            var responseContent = await response.Content.ReadAsStringAsync();
            return TryDeserializeObject<Response>(responseContent);
        }

        private static T TryDeserializeObject<T>(string responseContent)
        {
            try
            {
                var responseObject = JsonConvert.DeserializeObject<T>(responseContent);
                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception(responseContent, ex);
            }
        }

        private async Task<HttpResponseMessage> HttpPostAsync(object obj, string route)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(ShipmentBaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credential.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();
                httpClient.Timeout = Timeout;

                var message = obj.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }
    }
}
