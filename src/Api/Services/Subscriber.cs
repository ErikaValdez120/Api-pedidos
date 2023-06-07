using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Andreani.ARQ.AMQStreams.Interface;
using Andreani.Scheme.Onboarding;
using IPublisher = Andreani.ARQ.AMQStreams.Interface.IPublisher;
using api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Update;

namespace Api.Services { 
    
    public class Subscriber: ISubscriber {
    
        private ILogger <Subscriber> _logger;
        private IPublisher _publisher;
        private readonly ISender _mediator;

        public Subscriber(ILogger <Subscriber> logger, IPublisher publisher, ISender mediator)
        {
            _logger = logger;
            _publisher = publisher;
            _mediator = mediator;
        }


        public async Task RecivePedidoCreado(Pedido @event)
        {
            await _mediator.Send(new UpdatePedidoCommand() { Pedido = @event });
        }

    }



}
