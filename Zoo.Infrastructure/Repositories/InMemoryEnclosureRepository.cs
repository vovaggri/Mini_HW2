using Zoo.Application.Interfaces;
using Zoo.Domain.Entities;

namespace Zoo.Infrastructure.Repositories;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly List<Enclosure> _store = new();
    public Task<Enclosure> GetByIdAsync(Guid id)
    {
        var enclosure = _store.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(enclosure);
    }

    public Task<IEnumerable<Enclosure>> GetAllAsync() => Task.FromResult<IEnumerable<Enclosure>>(_store);

    public Task AddAsync(Enclosure enclosure)
    {
        _store.Add(enclosure);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Enclosure enclosure)
    {
        var index = _store.FindIndex(e => e.Id == enclosure.Id);
        if (index >= 0)
        {
            _store[index] = enclosure;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _store.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }
}