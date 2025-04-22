using Zoo.Domain.ValueObjects;

namespace Zoo.Presentation.DTOs.Animals;

public record FeedAnimalDto(
    DateTime FeedingTime,
    FoodType   FoodType
);