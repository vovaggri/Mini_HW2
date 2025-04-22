namespace Zoo.Domain.ValueObjects;

public sealed record Species(string Value) : ValueObject
{
    public string Value { get; } = 
        string.IsNullOrWhiteSpace(Value)
            ? throw new ArgumentException("Animal name cannot be empty.", nameof(Value))
            : Value.Trim();
}