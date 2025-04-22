using Zoo.Domain.Interfaces;

namespace Zoo.Domain.Events;

public class AnimalMovedEvent : IDomainEvent
{
    public Guid AnimalId { get; }
    public Guid? FromEnclosureId { get; }
    public Guid? ToEnclosureId { get; }
    public DateTime OccuredOn { get; }

    public AnimalMovedEvent(Guid animalId, Guid? fromId, Guid? toId)
    {
        AnimalId = animalId;
        FromEnclosureId = fromId;
        ToEnclosureId = toId;
        OccuredOn = DateTime.UtcNow;
    }
}