using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Utils;
using NSE.MessageBus;

namespace NSE.Identidade.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //extensão personalizada para pegar a conexão do messageQueue
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}
