using Zoo.Application.Interfaces;
using Zoo.Domain.Entities;
using Zoo.Domain.Events;
using Zoo.Domain.ValueObjects;

namespace Zoo.Application.Services;

public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _schedules;
    private readonly IEventDispatcher _events;
    
    public FeedingOrganizationService(
        IFeedingScheduleRepository schedules,
        IEventDispatcher events)
    {
        _schedules = schedules;
        _events = events;
    }

    public async Task ScheduleAsync(Guid animalId, DateTime time, FoodType foodType)
    {
        var fs = new FeedingSchedule(Guid.NewGuid(), animalId, time, foodType);
        await _schedules.AddAsync(fs);
    }

    public async Task FeedAsync(Guid scheduleId)
    {
        var fs = await _schedules.GetByIdAsync(scheduleId);
        fs.MarkCompleted();
        await _schedules.UpdateAsync(fs);
        await _events.DispatchAsync(new[] { new FeedingTimeEvent(fs.AnimalId, fs.FeedingTime) });
    }
    
    public Task<IEnumerable<FeedingSchedule>> GetAllAsync() => _schedules.GetAllAsync();
}