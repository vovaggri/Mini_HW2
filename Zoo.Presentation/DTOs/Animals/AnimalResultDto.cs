using Zoo.Domain.Enums;
using Zoo.Domain.ValueObjects;

namespace Zoo.Presentation.DTOs.Animals;

// DTO for output
public record AnimalResultDto(
    Guid Id, 
    Species Species,
    AnimalName Name,
    DateTime birthDate,
    Gender Gender,
    FoodType FavoriteFood,
    HealthStatus Status,
    Guid? EnclosureId
);