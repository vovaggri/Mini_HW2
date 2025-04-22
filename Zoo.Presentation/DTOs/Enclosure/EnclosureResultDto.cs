using Zoo.Domain.Enums;

namespace Zoo.Presentation.DTOs.Enclosure;

public record EnclosureResultDto(
    Guid Id,
    EnclosureType Type,
    double Size,
    int CurrentAnimalCount,
    int MaxCapacity
);