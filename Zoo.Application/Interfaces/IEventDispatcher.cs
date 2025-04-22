using Zoo.Domain.Interfaces;

namespace Zoo.Application.Interfaces;

public interface IEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
}