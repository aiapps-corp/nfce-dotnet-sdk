using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aiapps.Nfce.Api
{
    public class ProdutoApi : TokenApi
    {
        private string _routeEntregar = "api/produtos/entregar";

        private Credencial _credencial;

        public ProdutoApi(Credencial credencial)
        {
            _credencial = credencial ?? new Credencial();
        }

        public async Task<Retorno> CadastrarOuAtualizarAsync(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await HttpAtualizarAsync(produto);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                response = await HttpAtualizarAsync(produto);
            }

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
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await HttpRemoverAsync(id);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                response = await HttpRemoverAsync(id);
            }
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
            if (string.IsNullOrWhiteSpace(_credencial.Token))
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);

            var response = await HttpEntregarAsync(entrega);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _credencial.Token = await Token(_credencial.Email, _credencial.Senha);
                response = await HttpEntregarAsync(entrega);
            }
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
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

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
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

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
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

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
                httpClient.DefaultRequestHeaders.ConfigAuthorizationBearer(_credencial.Token);
                httpClient.DefaultRequestHeaders.AcceptApplicationJson();

                var message = entrega.AsJson();
                var response = await httpClient.PostAsync(_routeEntregar, message);
                return response;
            }
        }
    }
}
