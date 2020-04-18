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
        private string _route = "api/nfce";
        private string _routeCancel = "api/nfce/cancelar";
        private string _routeDanfe = "api/nfce/baixardanfe";

        private Credencial _credencial;
        private bool _shouldUseCache = true;
        public NfceApi(Credencial credencial)
        {
            _credencial = credencial;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nfce"></param>
        /// <param name="shouldUseCache"></param>
        /// <returns></returns>
        public async Task<NfceResultado> EmitirAsync(Nfce nfce)
        {
            var result = new NfceResultado();
            try
            {
                if (string.IsNullOrWhiteSpace(_credencial.Email) &&
                    string.IsNullOrWhiteSpace(_credencial.Senha))
                    throw new InvalidOperationException("Credêncial inválida para emissão de nfc-e");

                if (string.IsNullOrWhiteSpace(_credencial.Token))
                    _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

                using (var httpClient = new HttpClient(clientHandler, false))
                {
                    httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _credencial.Token);
                    httpClient.DefaultRequestHeaders
                          .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var message = nfce.AsJson();
                    var response = await httpClient.PostAsync(_route, message);

                    if (_shouldUseCache && response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        _shouldUseCache = false;
                        _credencial.Token = null;
                        return await EmitirAsync(nfce);
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<NfceResultado>(responseContent);
                    if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        result.Erro = $"Pedido {nfce.Referencia} já foi enviado";
                    }
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var obj = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        result.Erro = $"{obj?.message}";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Erro = ex.Message;
            }
            return result;
        }
    }
}
