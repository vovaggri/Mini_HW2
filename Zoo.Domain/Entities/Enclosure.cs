using Zoo.Domain.Enums;

namespace Zoo.Domain.Entities;

public class Enclosure
{
    public Guid Id { get; }
    public EnclosureType Type { get; private set; }
    public double Size { get; private set; }
    public int CurrentAnimalCount { get; private set; }
    public int MaxCapacity { get; private set; }

    public Enclosure(Guid id, EnclosureType type, double size, int maxCapacity)
    {
        Id = id;
        Type = type;
        Size = size;
        MaxCapacity = maxCapacity;
        CurrentAnimalCount = 0;
    }

    public void AddAnimal()
    {
        if (CurrentAnimalCount >= MaxCapacity)
            throw new InvalidOperationException("Enclosure is full.");
        CurrentAnimalCount++;
    }
    
    public void RemoveAnimal()
    {
        if (CurrentAnimalCount <= 0)
            throw new InvalidOperationException("No animals to remove.");
        CurrentAnimalCount--;
    }
    
    public void Clean()
    {
        // например, записать дату последней уборки
    }
}