using Zoo.Domain.ValueObjects;

namespace Zoo.Domain.Entities;

public class FeedingSchedule
{
    public Guid Id         { get; }
    public Guid AnimalId   { get; private set; }
    public DateTime FeedingTime { get; private set; }
    public FoodType FoodType     { get; private set; }
    public bool IsCompleted    { get; private set; }

    public FeedingSchedule(Guid id, Guid animalId, DateTime time, FoodType foodType)
    {
        Id = id;
        AnimalId = animalId;
        FeedingTime = time;
        FoodType = foodType;
        IsCompleted = false;
    }

    public void ChangeSchedule(DateTime newTime)
    {
        FeedingTime = newTime;
        IsCompleted = false;
    }

    public void MarkCompleted()
    {
        IsCompleted = true;
    }
}