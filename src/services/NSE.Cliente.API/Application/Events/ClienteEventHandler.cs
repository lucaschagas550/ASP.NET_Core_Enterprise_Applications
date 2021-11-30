using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Application.Events
{
    public class ClienteEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação, poderia enviar o envio de email etc
            return Task.CompletedTask;
        }
    }
}