using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Update;
using api_pedidos.Domain.Common;
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

namespace api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Update
{
    public class UpdatePedidoCommand : IRequest<Response<string>>
    {
        public Guid id { get; set; }
        public int numeroDePedido { get; set; }
        public string cicloDelPedido { get; set; }

        public Int64 codigoDeContratoInterno { get; set; }
        public int? estadoDelPedido { get; set; }
        public string cuentaCorriente { get; set; }    
        public DateTime cuando { get; set; }
    }
    }
    public class UpdatePedidoHandler : IRequestHandler<UpdatePedidoCommand, Response<string>>
    {
        private readonly ITransactionalRepository _repository;
        private readonly IReadOnlyQuery _query;
        private readonly ILogger<UpdatePedidoHandler> _logger;

        public UpdatePedidoHandler(ITransactionalRepository repository, IReadOnlyQuery query, ILogger<UpdatePedidoHandler> logger)
        {
            _repository = repository;
            _query = query;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _query.GetByIdAsync<Pedido>(nameof(request.numeroDePedido), request.numeroDePedido);
            var response = new Response<string>();
            if (pedido is null)
            {
                response.AddNotification("#3123", nameof(request.numeroDePedido), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Pedido", request.numeroDePedido));
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            pedido.id = request.id ;
            pedido.numeroDePedido = request.numeroDePedido;
            pedido.cicloDelPedido = request.cicloDelPedido;
            pedido.codigoDeContratoInterno = request.codigoDeContratoInterno;
            pedido.estadoDelPedido = request.estadoDelPedido;
            pedido.cuando = request.cuando;

            _repository.Update(pedido);
            await _repository.SaveChangeAsync();

            return response;
        }
    }


