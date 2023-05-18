using System;

namespace WebApi.Models;

public class UpdatePedidoVm
{
    public Guid id { get; set; }
    public int numeroDePedido { get; set; }
    public string cicloDelPedido { get; set; }
    public int codigoDeContratoInterno { get; set; }
    public int estadoDelPedido { get; set; }
    public string cuentaCorriente { get; set; }
    public DateTime cuando { get; set; }
}
