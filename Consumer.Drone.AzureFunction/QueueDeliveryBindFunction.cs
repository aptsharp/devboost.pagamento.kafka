using Consumer.Drone.AzureFunction.Config;
using Consumer.Drone.AzureFunction.Dto;
using Consumer.Drone.AzureFunction.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Consumer.Drone.AzureFunction
{
    public class QueueDeliveryBindFunction
    {
        static string Token;

        readonly string _kafkaHost;
        readonly int _kafkaPort;
        readonly string _kafkaTopic;
        readonly string _userToken;
        readonly string _passwordToken;

        readonly IPedidoService _pedidoService;
        readonly IAutorizationService _autorizationService;
        readonly IConfiguration _configuration;

        public QueueDeliveryBindFunction()
        {
            var _serviceProvider = new StartInjection().ServiceProvider;

            _configuration = _serviceProvider.GetService<IConfiguration>();
            _pedidoService = _serviceProvider.GetService<IPedidoService>();
            _autorizationService = _serviceProvider.GetService<IAutorizationService>();

            _kafkaHost = _configuration["Kafka:host"];
            _kafkaTopic = _configuration["Kafka:topic"];
            int.TryParse(_configuration["Kafka:port"], out _kafkaPort);

            _userToken = _configuration["EndPointDroneDelivery:UserToken"];
            _passwordToken = _configuration["EndPointDroneDelivery:PaswwordToken"];

            TokenConsumer();
        }

        public void TokenConsumer()
        {
            if (string.IsNullOrEmpty(Token))
            {
                Token = _autorizationService.GetToken(new AuthenticationRequest
                {
                    UserName = _userToken,
                    Password = _passwordToken
                }).Result;
            }
        }

        [FunctionName(nameof(SampleConsumer))]
        public void SampleConsumer([KafkaTrigger(
            "omv.serveblog.net:29092",
            "devboost.delivery.pedido",
            ConsumerGroup = "CriarPedido",
            Protocol = BrokerProtocol.Plaintext)] KafkaEventData<string> kafkaEvent,
            ILogger logger)
        {
            var valuePedido = kafkaEvent.Value.ToString();
            logger.LogInformation(valuePedido);
            _pedidoService.RealizarPedido(Token, valuePedido).Wait();
        }
    }
}
