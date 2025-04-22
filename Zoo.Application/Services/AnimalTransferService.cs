using Zoo.Application.Interfaces;

namespace Zoo.Application.Services;

public class AnimalTransferService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;
    private readonly IEventDispatcher _events;
    
    public AnimalTransferService(
        IAnimalRepository animals,
        IEnclosureRepository enclosures,
        IEventDispatcher events)
    {
        _animals = animals;
        _enclosures = enclosures;
        _events = events;
    }

    public async Task TransferAsync(Guid animalId, Guid targetEnclosureId)
    {
        var animal = await _animals.GetByIdAsync(animalId);
        var fromId = animal.EnclosureId;

        var target = await _enclosures.GetByIdAsync(targetEnclosureId);
        if (target.CurrentAnimalCount >= target.MaxCapacity)
        {
            throw new InvalidOperationException("The target animal is full.");
        }

        if (fromId.HasValue)
        {
            var from = await _enclosures.GetByIdAsync(fromId.Value);
            from.RemoveAnimal();
            await _enclosures.UpdateAsync(from);
        }
        
        target.AddAnimal();
        await _enclosures.UpdateAsync(target);
        
        animal.MoveTo(targetEnclosureId);
        await _animals.UpdateAsync(animal);

        await _events.DispatchAsync(animal.DomainEvents);
        animal.ClearEvents();
    }
}