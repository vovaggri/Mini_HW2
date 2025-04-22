using Zoo.Application.Interfaces;
using Zoo.Domain.Entities;

namespace Zoo.Infrastructure.Repositories;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly List<FeedingSchedule> _store = new();
    
    public Task<FeedingSchedule> GetByIdAsync(Guid id)
    {
        var schedule = _store.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(schedule);
    }

    public Task<IEnumerable<FeedingSchedule>> GetAllAsync() => Task.FromResult<IEnumerable<FeedingSchedule>>(_store);

    public Task AddAsync(FeedingSchedule schedule)
    {
        _store.Add(schedule);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(FeedingSchedule schedule)
    {
        var index = _store.FindIndex(s => s.Id == schedule.Id);
        if (index >= 0)
        {
            _store[index] = schedule;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _store.RemoveAll(s => s.Id == id);
        return Task.CompletedTask;
    }
}