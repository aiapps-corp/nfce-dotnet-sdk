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

        public async Task CadastrarOuAtualizarAsync(Produto produto)
        {
            var response = await HttpAtualizarAsync(produto);
            if (response.StatusCode == HttpStatusCode.NotFound)
                response = await HttpCadastrarAsync(produto);

            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException("Falha ao sincronizar produto (Emissão de NFC-e)", new Exception(responseContent));
            }
        }

        public async Task Remover(string id)
        {
            var error = "Falha ao remover produto (Emissão de NFC-e)";
            var response = await HttpRemoverAsync(id);
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException(error, new Exception(responseContent));
            }
        }

        public async Task Entregar(Entrega entrega)
        {
            var response = await HttpEntregarAsync(entrega);
            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException("Falha ao entregar produto (Emissão de NFC-e)", new Exception(responseContent));
            }
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

                var url = $"api/produtos?id={(produto.Referencia)}";
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
