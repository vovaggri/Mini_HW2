namespace Zoo.Domain.ValueObjects;

public sealed record FoodType(string Value) : ValueObject
{
    public string Value { get; } = 
        string.IsNullOrWhiteSpace(Value)
            ? throw new ArgumentException("Food type cannot be empty.", nameof(Value))
            : Value.Trim();
}