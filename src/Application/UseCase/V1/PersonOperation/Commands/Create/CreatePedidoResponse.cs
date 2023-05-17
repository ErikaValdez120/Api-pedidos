using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Create
{
    //public record struct CreatePedidoResponse( string cuentaCorriente, int codigoDeContratoInterno) { }

    public record struct CreatePedidoResponse(Guid PedidoId, string Message)
    {
    }
}
