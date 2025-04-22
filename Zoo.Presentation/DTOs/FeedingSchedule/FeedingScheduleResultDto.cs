using Zoo.Domain.ValueObjects;

namespace Zoo.Presentation.DTOs.FeedingSchedule;

public record FeedingScheduleResultDto(
    Guid   Id,
    Guid   AnimalId,
    DateTime FeedingTime,
    FoodType FoodType,
    bool   IsCompleted
);