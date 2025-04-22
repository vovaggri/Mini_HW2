using Zoo.Domain.Entities;

namespace Zoo.Application.Interfaces;

public interface IFeedingScheduleRepository
{
    Task<FeedingSchedule> GetByIdAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> GetAllAsync();
    Task AddAsync(FeedingSchedule schedule);
    Task UpdateAsync(FeedingSchedule schedule);
    Task DeleteAsync(Guid id);
}