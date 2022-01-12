using Aiapps.Sdk.Api;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Licensing.Api
{
    public class LicenseApi : TokenApi
    {
        private Credential _credential;
        private string route = "api/license";

        public LicenseApi(Credential credential)
        {
            _credential = credential ?? new Credential();
            BaseHttpsAddress = "http://licensing.aiapps.com.br";
        }

        public async Task Connect(ConnectLicenseRequest value)
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
                  var r = await HttpPostAsync(value, $"{route}/connect", _credential.Token);
                  return r;
              });
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception(responseContent);
            }
        }

        public async Task Activate(ActivateLicenseRequest value)
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
                  var r = await HttpPostAsync(value, $"{route}/activate", _credential.Token);
                  return r;
              });
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception(responseContent);
            }
        }

        public async Task Cancel(CancelLicenseRequest value)
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
                  var r = await HttpPostAsync(value, $"{route}/cancel", _credential.Token);
                  return r;
              });
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception(responseContent);
            }
        }
    }
}
