using Zoo.Domain.Enums;

namespace Zoo.Presentation.DTOs.Enclosure;

public record CreateEnclosureDto(
    EnclosureType Type,
    double Size,
    int MaxCapacity
);