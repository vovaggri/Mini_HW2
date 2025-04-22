using Zoo.Domain.Enums;
using Zoo.Domain.ValueObjects;

namespace Zoo.Presentation.DTOs.Animals;

public record CreateAnimalDto(
    Species Species,
    AnimalName Name,
    DateTime BirthDate,
    Gender Gender,
    FoodType FavoriteFood
);