using api_pedidos.Domain.Common;
using System.Threading.Tasks;

namespace api_pedidos.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
