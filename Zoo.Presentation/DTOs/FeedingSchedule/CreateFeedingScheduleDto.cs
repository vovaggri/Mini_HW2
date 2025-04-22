using Zoo.Domain.ValueObjects;

namespace Zoo.Presentation.DTOs.FeedingSchedule;

public record CreateFeedingScheduleDto(
    Guid   AnimalId,
    DateTime FeedingTime,
    FoodType FoodType
);