using System.Runtime.InteropServices;
using Andreani.ARQ.Pipeline.Clases;
using Andreani.ARQ.WebHost.Controllers;
using api_pedidos.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_pedidos.Application.UseCase.V1.PersonOperation.Queries.GetList;
using api_pedidos.Application.UseCase.V1.PersonOperation.Commands.Create;
using System;

namespace Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PedidoController : ApiControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(List<PedidoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPedido()));

    [HttpPost]
    [ProducesResponseType(typeof(CreatePedidoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreatePedidoCommand body) => Result(await Mediator.Send(body));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => this.Result(await Mediator.Send(new GetPedido() { Id = id }));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => this.Result(await Mediator.Send(new DeletePedido() { Id = id }));


}


