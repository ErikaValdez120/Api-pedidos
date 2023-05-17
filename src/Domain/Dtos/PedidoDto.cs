using api_pedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_pedidos.Domain.Dtos
{
    
        public record struct PedidoDto(
            
            Guid id,
            int? numeroDePedido,
            string cicloDelPedido,
            Int64 codigoDeContratoInterno,
            int? estadoDelPedido,
            string cuentaCorriente,
            DateTime Cuando
           ){ }
   
}
