using Zoo.Domain.Enums;
using Zoo.Domain.Events;
using Zoo.Domain.Interfaces;
using Zoo.Domain.ValueObjects;

namespace Zoo.Domain.Entities;

public class Animal
{
    public Guid Id { get; private set; }
    public Species Species { get; private set; }
    public AnimalName Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public FoodType FavoriteFood { get; private set; }
    public HealthStatus Status { get; private set; }
    public Guid? EnclosureId { get; private set; }
    
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.AsReadOnly();

    public Animal(Guid id, Species species, Gender gender, AnimalName name, DateTime birthDate, FoodType favoriteFood)
    {
        Id = id;
        Species = species;
        Gender = gender;
        Name = name;
        BirthDate = birthDate;
        FavoriteFood = favoriteFood;
        Status = HealthStatus.Healthy;
    }

    public void Feed(FoodType food)
    {
        if (food == FavoriteFood)
        {
            Status = HealthStatus.Healthy;
        }

        _events.Add(new FeedingTimeEvent(Id, DateTime.Now));
    }

    public void Heal()
    {
        Status = HealthStatus.Healthy;
    }

    public void MoveTo(Guid newEnclosureId)
    {
        var oldId = EnclosureId;
        EnclosureId = newEnclosureId;
        _events.Add(new AnimalMovedEvent(Id, oldId, newEnclosureId));
    }
    
    public void ClearEvents() => _events.Clear();
}