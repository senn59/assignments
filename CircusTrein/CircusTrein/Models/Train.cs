namespace CircusTrein.Models;

public class Train
{
    private List<Wagon> _wagons = new List<Wagon>();
    
    public IReadOnlyList<Wagon> Wagons => _wagons;
    public int Size => Wagons.Count;
    private List<Animal> _animals;

    public Train(List<Animal> animals)
    {
        _animals = GetSortedAnimals(animals);
        LoadAnimals();
    }
    
    private void LoadAnimals()
    {
        foreach (var animal in _animals)
        {
            if (animal.Type == AnimalType.Carnivore)
            {
                var wagon = new Wagon();
                wagon.TryFitAnimal(animal);
                _wagons.Add(wagon);
                continue;
            }

            if (!TryPutInExistingWagon(animal))
            {
                var wagon = new Wagon();
                wagon.TryFitAnimal(animal);
                _wagons.Add(wagon);
            }
            
        }
    }

    private bool TryPutInExistingWagon(Animal animal)
    {
        var foundWagon = false;
        foreach (var wagon in Wagons)
        {
            if (foundWagon)
            {
                return foundWagon;
            }
            var carnivore = wagon.Animals.FirstOrDefault(a => a.Type == AnimalType.Carnivore);
            if (carnivore != null)
            {
                if (animal.Size <= carnivore.Size) continue;
            }
            if ((int) animal.Size + wagon.GetTotalSize() > wagon.MaxSize) continue;
            foundWagon = true;
            wagon.TryFitAnimal(animal);
        }

        return foundWagon;
    }

    private List<Animal> GetSortedAnimals(List<Animal> animals)
    {
        var mediumHerbsCount = animals.Count(a => a is { Size: AnimalSize.Medium, Type: AnimalType.Herbivore });
        var carnivoreCount = animals.Count(a => a.Type == AnimalType.Carnivore);
        
        IEnumerable<Animal> sortedAnimals = animals
            .OrderByDescending(a => a.Type)
            .ThenBy(a => a.Size);

        if (mediumHerbsCount < 3 || carnivoreCount == 0)
        {
            sortedAnimals = animals
                .OrderByDescending(a => a.Type)
                .ThenByDescending(a => a.Size);
        }

        return sortedAnimals.ToList();
    }
}