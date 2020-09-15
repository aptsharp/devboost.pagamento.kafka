using Consumer.Drone.AzureFunction.Services.Interfaces;
using DroneDelivery.Helper.HttpUtils;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Drone.AzureFunction.Services
{
    public class PedidoService : IPedidoService
    {
        readonly string _url;

        public PedidoService(string url)
        {
            _url = (string.IsNullOrEmpty(url))
                ? "https://localhost:44391/api/pedido/realizarpedido"
                : url;
        }

        public async Task<HttpResponseMessage> RealizarPedido(string token, string jsonBody)
        {
            var pedidoJson = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var authentication = new AuthenticationHeaderValue("bearer", token);
            var result = HttpPostService.PostService(_url, pedidoJson, authentication);
            return await Task.FromResult(result);
        }
    }
}
