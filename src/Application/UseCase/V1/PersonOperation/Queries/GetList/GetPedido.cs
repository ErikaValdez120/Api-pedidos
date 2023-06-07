using Andreani.ARQ.Core.Interface;
using Andreani.ARQ.Pipeline.Clases;
using api_pedidos.Domain.Dtos;
using api_pedidos.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace api_pedidos.Application.UseCase.V1.PersonOperation.Queries.GetList
{
    public record struct GetPedido : IRequest<Response<PedidoDto>>
    {
        public Guid Id { get; set; }

        public GetPedido(Guid pedidoId)
        {

            Id = pedidoId;
        }
    }

    public class GetPedidoHandler : IRequestHandler<GetPedido, Response<PedidoDto>>
    {
        private readonly IReadOnlyQuery _query;

        public GetPedidoHandler(IReadOnlyQuery query)
        {
            _query = query;
        }

        public async Task<Response<PedidoDto>> Handle(GetPedido request, CancellationToken cancellationToken)
        {
            var result = await _query.GetByIdAsync<Pedido>(nameof(request.Id), request.Id);



            var sqlString = $"select * from dbo.EstadoDelpedido where id = '{result.estadoDelPedido}'";
            var resultadoEstadoDelPedido = await _query.FirstOrDefaultQueryAsync<EstadoDelPedido>(sqlString);




            PedidoDto pedidodto = new PedidoDto()
            {



                id = result.id,
                numeroDePedido = result.numeroDePedido,
                cicloDelPedido = result.cicloDelPedido,
                codigoDeContratoInterno = result.codigoDeContratoInterno,
                estadoDelPedido = result.estadoDelPedido,
                cuentaCorriente = result.cuentaCorriente,
                cuando = result.cuando


            };
            return new Response<PedidoDto>
            {
                Content = pedidodto,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}

