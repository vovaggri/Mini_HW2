using Zoo.Application.Interfaces;
using Zoo.Domain.Entities;

namespace Zoo.Infrastructure.Repositories;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly List<Animal> _store = new();
    
    public Task<Animal> GetByIdAsync(Guid id) => Task.FromResult(_store.FirstOrDefault(a => a.Id == id));

    public Task<IEnumerable<Animal>> GetAllAsync() => Task.FromResult<IEnumerable<Animal>>(_store);

    public Task AddAsync(Animal animal)
    {
        _store.Add(animal);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Animal animal)
    {
        var index = _store.FindIndex(a => a.Id == animal.Id);
        if (index >= 0)
        {
            _store[index] = animal;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _store.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }
}