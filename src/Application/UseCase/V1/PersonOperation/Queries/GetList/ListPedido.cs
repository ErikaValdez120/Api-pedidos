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
    //public class
    public record struct ListPedido : IRequest<Response<List<PedidoDto>>>
    {
    }
    public class ListPedidoHandler : IRequestHandler<ListPedido, Response<List<PedidoDto>>>
    {
        private readonly IReadOnlyQuery _query;
        public ListPedidoHandler(IReadOnlyQuery query)
        {
            _query = query;
        }
        public async Task<Response<List<PedidoDto>>> Handle(ListPedido request, CancellationToken cancellationToken)
        {
            var result = await _query.GetAllAsync<PedidoDto>(nameof(Pedido)); // trae toda la lista pedidoDto
            
         
            return new Response<List<PedidoDto>>
            {
                //resultadoPedidos,
                Content = result.ToList(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}


