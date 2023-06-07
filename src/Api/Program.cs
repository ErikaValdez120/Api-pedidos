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

builder.Services.AddKafka(builder.Configuration)
 .CreateOrUpdateTopic(6, "PedidoCreadoV")
 .ToProducer<Pedido>("PedidoCreadoV") // esta configuracion es un publicador
 .ToConsumer<Subscriber,Pedido>("PedidoAsignadoV") // consumo mensaje, 
 .Build();
var app = builder.Build();

app.ConfigureAndreani();

app.Run();

