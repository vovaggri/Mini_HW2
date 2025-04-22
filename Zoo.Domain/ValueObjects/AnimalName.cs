namespace Zoo.Domain.ValueObjects;

// Название животного (непустая строка)
public sealed record AnimalName(string Value) : ValueObject
{
    public string Value { get; } = 
        string.IsNullOrWhiteSpace(Value)
            ? throw new ArgumentException("Animal name cannot be empty.", nameof(Value))
            : Value.Trim();
}