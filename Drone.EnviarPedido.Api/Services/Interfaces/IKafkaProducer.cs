using Confluent.Kafka;
using Drone.EnviarPedido.Api.Dto;
using System.Threading.Tasks;

namespace Drone.EnviarPedido.Api.Services.Interfaces
{
    public interface IKafkaProducer
    {
        Task<DeliveryResult<Confluent.Kafka.Null, string>> RealizarPedido(PedidoDto pedido);
    }
}
