using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_pedidos.Domain.Dtos
{
    public record struct EstadoDelPedidoDto(int Id, string Descripcion)
    {
    }
}
