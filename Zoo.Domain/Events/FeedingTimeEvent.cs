using Zoo.Domain.Interfaces;

namespace Zoo.Domain.Events;

public class FeedingTimeEvent : IDomainEvent
{
    public Guid AnimalId { get; }
    public DateTime FeedingTime { get; }

    public FeedingTimeEvent(Guid animalId, DateTime feedingTime)
    {
        AnimalId = animalId;
        FeedingTime = feedingTime;
    }
}