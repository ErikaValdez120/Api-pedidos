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
        public int NumeroPedido { get; set; }
        public Guid cicloDePedido { get; set; }

        public int CodigoDeContratoInterno { get; set; }
        public int EstadoDelPedido { get; set; }
        public int CuentaCorriente { get; set; }    
        public DateOnly cuando { get; set; }
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
            var pedido = await _query.GetByIdAsync<Pedido>(nameof(request.NumeroPedido), request.NumeroPedido);
            var response = new Response<string>();
            if (pedido is null)
            {
                response.AddNotification("#3123", nameof(request.NumeroPedido), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Pedido", request.NumeroPedido));
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
        //    pedido.id = request.Id ;
            pedido.numeroDePedido = request.NumeroPedido;
          //  pedido.cicloDePedido = request.CicloDePedido;
            pedido.codigoDeContratoInterno = request.CodigoDeContratoInterno;
            pedido.estadoDelPedido = request.EstadoDelPedido;
            //pedido.cuando = request.Cuando;

            _repository.Update(pedido);
            await _repository.SaveChangeAsync();

            return response;
        }
    }


