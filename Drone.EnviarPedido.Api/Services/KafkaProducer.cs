using Confluent.Kafka;
using Drone.EnviarPedido.Api.Services.Interfaces;
using Drone.EnviarPedido.Api.Dto;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Drone.EnviarPedido.Api.Services
{
    public class KafkaProducer : IKafkaProducer
    {
        readonly string _host;
        readonly int _port;
        readonly string _topic;

        public KafkaProducer(string host, int port, string topic)
        {
            _host = host;
            _port = port;
            _topic = topic;
        }

        public async Task<DeliveryResult<Null, string>> RealizarPedido(PedidoDto pedido)
        {
            if (pedido == null)
                return null;

            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = $"{_host}:{_port}"
            };

            using IProducer<Null, string> producer = new ProducerBuilder<Null, string>(config).Build();
            var result = await producer.ProduceAsync(
                _topic,
                new Message<Null, string>
                {
                    Value = ConvertPedidoToJson(pedido)
                }
            );

            return result;
        }

        private string ConvertPedidoToJson(PedidoDto pedido) =>
            JsonConvert.SerializeObject(pedido);
    }
}
