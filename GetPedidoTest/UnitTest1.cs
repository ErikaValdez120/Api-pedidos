using Andreani.ARQ.Core.Interface;
using Andreani.Scheme.Onboarding;
using api_pedidos.Application.UseCase.V1.PersonOperation.Queries.GetList;
using api_pedidos.Domain.Entities;
using Moq;
using Pedido = api_pedidos.Domain.Entities.Pedido;

namespace GetPedidoTest
{
    public class GetPedidoTest
    {
        [Fact]
        public async void Handle_ValidRequest_ResponsePedidoDto() // funcion a testear
        {
            // Arrange

            //Moq es una librería que permite generar objetos "Mock" que sustituyan a las dependencias de
            //nuestra clase a probar.
            var _query = new Mock<IReadOnlyQuery>();
            var _handler = new GetPedidoHandler(_query.Object);
            var id = new Guid();
            var request = new GetPedido(id);

            var pedido = new Pedido
            {
                id = id,// el schema de pedido andreani tiene otros tipos de datos
                numeroDePedido = 1,
                cicloDelPedido = id.ToString(),
                codigoDeContratoInterno = 123456789,
                estadoDelPedido = 1,
                cuentaCorriente = 1234,
                cuando = DateTime.Now,

            };

            _query.Setup(q => q.GetByIdAsync<api_pedidos.Domain.Entities.Pedido>(id)).ReturnsAsync((pedido));

            //Act
            var resultado = await _handler.Handle(request,CancellationToken.None);

            //Assert
            Assert.NotNull(resultado);
        }
    }
}