using System;

namespace WebApi.Models;

public class UpdatePedidoVm
{
    public Guid id { get; set; }
    public int numeroPedido { get; set; }
    public Guid cicloDePedido { get; set; }
    public int codigoDeContratoInterno { get; set; }
    public string estadoDelPedido { get; set; }
    public int cuentaCorriente { get; set; }
    public DateOnly cuando { get; set; }
}
