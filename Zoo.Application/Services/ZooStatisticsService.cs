using Zoo.Application.Interfaces;

namespace Zoo.Application.Services;

public class ZooStatisticsService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public ZooStatisticsService(IAnimalRepository animals, IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public async Task<ZooStatistics> GetAsync()
    {
        var a = await _animals.GetAllAsync();
        var e = await _enclosures.GetAllAsync();

        return new ZooStatistics
        {
            TotalAnimals = a.Count(),
            FreeEnclosures = e.Count(x => x.CurrentAnimalCount < x.MaxCapacity)
        };
        
    }
}

