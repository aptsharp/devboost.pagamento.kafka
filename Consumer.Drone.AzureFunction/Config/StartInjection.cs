using Consumer.Drone.AzureFunction.Services;
using Consumer.Drone.AzureFunction.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Consumer.Drone.AzureFunction.Config
{
    public class StartInjection
    {
        public StartInjection()
        {
            BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; private set; }

        void BuildServiceProvider()
        {
            var _services = new ServiceCollection();

            IConfiguration configuration = StartConfiguration.Configuration;
            _services.AddSingleton(x => configuration);

            var urlAuthorization = configuration["EndPointDroneDelivery:urlAuthorization"];
            var urlRealizarPedido = configuration["EndPointDroneDelivery:urlRealizarPedido"];

            _services.AddScoped<IAutorizationService>(x => new AutorizationService(urlAuthorization));
            _services.AddScoped<IPedidoService>(x => new PedidoService(urlRealizarPedido));

            // Constroe o Provider
            ServiceProvider = _services.BuildServiceProvider();
        }
    }
}
