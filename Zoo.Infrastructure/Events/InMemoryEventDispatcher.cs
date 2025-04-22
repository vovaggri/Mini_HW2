using Zoo.Application.Interfaces;
using Zoo.Domain.Interfaces;

namespace Zoo.Infrastructure.Events;

public class InMemoryEventDispatcher : IEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            Console.WriteLine($"[Event] {domainEvent.GetType().Name} at {DateTime.UtcNow}");
        }
        return Task.CompletedTask;
    }
}