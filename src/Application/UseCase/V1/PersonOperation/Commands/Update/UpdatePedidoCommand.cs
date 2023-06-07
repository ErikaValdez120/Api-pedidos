using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Update;
using api_pedidos.Domain.Common;
using api_pedidos.Domain.Dtos;
using api_pedidos.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Update
{
    public class UpdatePedidoCommand : IRequest<SendPedido>
    {
        public Andreani.Scheme.Onboarding.Pedido Pedido { get; set; }
    }
    public class UpdatePedidoHandler : IRequestHandler<UpdatePedidoCommand,SendPedido>
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

        public async Task<SendPedido> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            Andreani.Scheme.Onboarding.Pedido pedidoActualizado = request.Pedido;
            var entidadPedido = new api_pedidos.Domain.Entities.Pedido()
            {
                id = Guid.Parse(pedidoActualizado.id),
                numeroDePedido = pedidoActualizado.numeroDePedido,
                cicloDelPedido = pedidoActualizado.cicloDelPedido,
                codigoDeContratoInterno = pedidoActualizado.codigoDeContratoInterno,
                estadoDelPedido = 2,
                cuentaCorriente = (int)pedidoActualizado.cuentaCorriente,
                cuando = DateTime.Parse(pedidoActualizado.cuando)
            };
            pedidoActualizado.estadoDelPedido = 2.ToString();
            // _repository --> transaccion en la base de datos
            _repository.Update(entidadPedido);
            await _repository.SaveChangeAsync();
            return new SendPedido();
        }

    }

    public class SendPedido
    {
    };

}

