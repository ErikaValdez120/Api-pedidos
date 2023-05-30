using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using Andreani.Scheme.Onboarding;

using Andreani.ARQ.AMQStreams.Extensions;
using Andreani.ARQ.AMQStreams.Class;
using Andreani.ARQ.AMQStreams.Interface;
using api_pedidos.Domain.Dtos;
using api_pedidos.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Create
{
    public class CreatePedidoCommand : IRequest<Response<CreatePedidoResponse>>
    {
   
        public int cuentaCorriente { get; set; }
      
        public  Int64 codigoDeContratoInterno { get; set; }
  
    }

    public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, Response<CreatePedidoResponse>>
    {
        private readonly ITransactionalRepository _repository;
        private readonly ILogger<CreatePedidoCommandHandler> _logger;
        private readonly Andreani.ARQ.AMQStreams.Interface.IPublisher _publish;

        public CreatePedidoCommandHandler(ITransactionalRepository repository, ILogger<CreatePedidoCommandHandler> logger, Andreani.ARQ.AMQStreams.Interface.IPublisher publish)
        {
            _repository = repository;
            _logger = logger;
            _publish = publish;
        }

        public async Task<Response<CreatePedidoResponse>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Pedido // PEDIDO
            {

                cuentaCorriente = request.cuentaCorriente,
                codigoDeContratoInterno = request.codigoDeContratoInterno,
                cuando=DateTime.Now,
                numeroDePedido = null,
                id=Guid.NewGuid(),
                estadoDelPedido=1
            };
            entity.cicloDelPedido = entity.id.ToString();
            _repository.Insert(entity);
            await _repository.SaveChangeAsync();
            _logger.LogDebug("El pedido fue agregado correctamente");
            // await _publish.To<Andreani.Scheme.Onboarding.Pedido>(entity, entity.id.ToString(), "PedidoCreado");
            var evento = new Andreani.Scheme.Onboarding.Pedido();

            evento.cicloDelPedido = entity.cicloDelPedido;
            evento.id = entity.id.ToString();
            evento.cuentaCorriente = entity.cuentaCorriente;
            evento.codigoDeContratoInterno = entity.codigoDeContratoInterno;
            evento.cuando = entity.cuando.ToString();
            evento.numeroDePedido = 0;
            evento.estadoDelPedido = entity.estadoDelPedido.ToString();

            // Publicación del objeto "evento" en el topic "PedidoCreado" utilizando el método _publish.To

            await _publish.To<Andreani.Scheme.Onboarding.Pedido>(evento, entity.id.ToString(), "PedidoCreado");


            return new Response<CreatePedidoResponse>
            {
                Content = new CreatePedidoResponse
                {

                    Message= "Success",
                    PedidoId=entity.id
                             
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}
