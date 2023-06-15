using Andreani.ARQ.AMQStreams.Extensions;
using Andreani.ARQ.WebHost.Extension;
using api_pedidos.Application;
using api_pedidos.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Andreani.Scheme.Onboarding;
using Api.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAndreaniWebHost(args);
builder.Services.ConfigureAndreaniServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddKafka(builder.Configuration)                                                                            //agrega el servicio de Kafka al contenedor de inyecci�n de dependencias.
 .CreateOrUpdateTopic(6,"PedidoCreadoV")//crea o actualiza un topic en Kafka. El 6 representa el n�mero de particiones del topic y "PedidoCreadoV" es el nombre del topic.
 .ToProducer<Pedido>("PedidoCreadoV") //configura el cliente Kafka como un productor. Indica que se va a publicar mensajes del tipo Pedido en el tema "PedidoCreadoV".
 .ToConsumer<Subscriber,Pedido>("PedidoAsignadoV") //configura el cliente Kafka como un consumidor.Indica que se va a consumir mensajes del tipo Pedido desde el tema "PedidoAsignadoV". El par�metro Subscriber representa la clase o componente que implementa la l�gica de consumo de mensajes.
 .Build();
var app = builder.Build();

app.ConfigureAndreani();

app.Run();

