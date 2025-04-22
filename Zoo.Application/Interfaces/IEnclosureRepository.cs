using Zoo.Domain.Entities;

namespace Zoo.Application.Interfaces;

public interface IEnclosureRepository
{
    Task<Enclosure> GetByIdAsync(Guid id);
    Task<IEnumerable<Enclosure>> GetAllAsync();
    Task AddAsync(Enclosure enclosure);
    Task UpdateAsync(Enclosure enclosure);
    Task DeleteAsync(Guid id);
}