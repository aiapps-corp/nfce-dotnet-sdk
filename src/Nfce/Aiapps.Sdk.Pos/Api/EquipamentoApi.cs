using Aiapps.Sdk;
using Aiapps.Sdk.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aiapps.Sdk.Pos.Api
{
    public class EquipamentoApi : TokenApi
    {
        private Credencial _credencial;
        private string route = "api/equipamentos";

        public EquipamentoApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
        }

        public async Task<Retorno> Registrar(Equipamento equipamento)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await HttpRegistrarAsync(equipamento);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                response = await HttpRegistrarAsync(equipamento);
            }
            var retorno = new Retorno { Sucesso = true };
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                retorno.Sucesso = false;
                retorno.Mensagem = $"Falha ao registrar equipamento {responseContent}";
            }
            return retorno;
        }

        private async Task<HttpResponseMessage> HttpRegistrarAsync(Equipamento equipamento)
        {
            using (var httpClient = new HttpClient(clientHandler, false))
            {
                httpClient.BaseAddress = new Uri(BaseHttpsAddress);
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

                var message = equipamento.AsJson();
                var response = await httpClient.PostAsync(route, message);
                return response;
            }
        }
    }
}
