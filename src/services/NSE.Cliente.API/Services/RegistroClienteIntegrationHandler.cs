using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        //quando mensagem eh enviado
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request 
                => await RegistrarCliente(request));// envia o request que eh o UsuarioRegistradoIntegrationEvent para chamar o command handler

            return Task.CompletedTask;
        }

        //chamar o command handler
        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clientCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso;

            //resolve o fato do AddHostedService ser um single e o CreateScope resolve problemas dentro do ciclo de vida do singleton
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clientCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
