using Aiapps.Sdk;
using Aiapps.Sdk.Api;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aiapps.Nfce.Api
{
    public class NfceApi : TokenApi
    {
        protected string route = "api/nfce";
        private string _routeCancel = "api/nfce/cancelar";
        private string _routeDanfe = "api/nfce/baixardanfe";

        private Credencial _credencial;

        public NfceApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
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
                if (string.IsNullOrWhiteSpace(_credencial.Email) &&
                    string.IsNullOrWhiteSpace(_credencial.Senha))
                    throw new InvalidOperationException("Credêncial inválida para emissão de nfc-e");

                if (string.IsNullOrWhiteSpace(_credencial.Token))
                    _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

                var response = await HttpEmitirAsync(pedido);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                    response = await HttpEmitirAsync(pedido);
                }

                responseContent = await response.Content.ReadAsStringAsync();
                nfce = JsonConvert.DeserializeObject<Nfce>(responseContent);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    nfce.Erro = $"Pedido {pedido.Referencia} já foi enviado";
                }
                if (response.StatusCode == HttpStatusCode.BadRequest && string.IsNullOrWhiteSpace(nfce.Sefaz.Motivo))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    nfce.Erro = $"{obj?.message}";
                }
            }
            catch (Exception ex)
            {
                nfce.Erro = $"{responseContent} {ex.Message}";
            }
            return nfce;
        }

        public async Task<bool> CancelarAsync(string chaveAcesso, string motivo)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await HttpCancelarAsync(chaveAcesso, motivo);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                response = await HttpCancelarAsync(chaveAcesso, motivo);
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<HttpResponseMessage> DanfeAsync(string chaveAcesso)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await HttpDanfeAsync(chaveAcesso);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                response = await HttpDanfeAsync(chaveAcesso);
            }
            return response;
        }

        private async Task<HttpResponseMessage> HttpEmitirAsync(Pedido pedido)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

                var message = pedido.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpCancelarAsync(string chaveAcesso, string motivo)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

                var response = await httpClient.PostAsync(_routeCancel, new { chave = chaveAcesso, motivo }.AsJson());
                return response;
            }
        }

        private async Task<HttpResponseMessage> HttpDanfeAsync(string chaveAcesso)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

                var response = await httpClient.GetAsync($"{_routeDanfe}?chaveAcesso={chaveAcesso}");
                return response;
            }
        }
    }
}
