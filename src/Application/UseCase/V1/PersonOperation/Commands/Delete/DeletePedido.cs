using api_pedidos.Domain.Dtos;
using Andreani.ARQ.Pipeline.Clases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andreani.ARQ.Core.Interface;
using api_pedidos.Domain.Entities;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Delete
{
    public record struct DeletePedido : IRequest<Response<Pedido>>
    {
        public Guid Id { get; set; }
    }

    public class DeLetePedidoHandler : IRequestHandler<DeletePedido, Response<Pedido>>
    {
        private readonly IReadOnlyQuery _query;
        private readonly ITransactionalRepository _repository;

        public DeLetePedidoHandler(ITransactionalRepository repository, IReadOnlyQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public async Task<Response<Pedido>> Handle(DeletePedido request, CancellationToken cancellationToken)
        {
            var result = await _query.GetByIdAsync<Pedido>(nameof(request.Id), request.Id);
            _repository.Delete(result);
            _repository.SaveChange();

            return new Response<Pedido>
            {

                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
