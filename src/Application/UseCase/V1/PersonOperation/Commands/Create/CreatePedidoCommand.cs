using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
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
   
        public string cuentaCorriente { get; set; }
      
        public  Int64 codigoDeContratoInterno { get; set; }
  
    }

    public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, Response<CreatePedidoResponse>>
    {
        private readonly ITransactionalRepository _repository;
        private readonly ILogger<CreatePedidoCommandHandler> _logger;

        public CreatePedidoCommandHandler(ITransactionalRepository repository, ILogger<CreatePedidoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreatePedidoResponse>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Pedido // PEDIDO
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

            return new Response<CreatePedidoResponse>
            {
                Content = new CreatePedidoResponse
                {

                    Message= "Success",
                    PedidoId=entity.id
                        /*

                    cuentaCorriente = entity.cuentaCorriente,
                    
                    codigoDeContratoInterno= entity.codigoDeContratoInterno,
                        */                 
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}
