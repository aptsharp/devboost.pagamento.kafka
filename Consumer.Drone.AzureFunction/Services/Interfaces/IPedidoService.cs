using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer.Drone.AzureFunction.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<HttpResponseMessage> RealizarPedido(string token, string jsonBody);
    }
}
