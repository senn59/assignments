namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    public int Size => Wagons.Count;
    private List<Animal> _animals;

    public Train(List<Animal> animals)
    {
        var mediumHerbsCount = animals.Count(a => a.Size == AnimalSize.Medium && a.Type == AnimalType.Herbivore);
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

        _animals = sortedAnimals.ToList();
        LoadAnimals();
    }
    
    private void LoadAnimals()
    {
        foreach (var animal in _animals)
        {
            if (animal.Type == AnimalType.Carnivore)
            {
                var wagon = new Wagon();
                wagon.Animals.Add(animal);
                Wagons.Add(wagon);
                continue;
            }
            
            var foundWagon = false;
            foreach (var wagon in Wagons)
            {
                if (foundWagon)
                {
                    break;
                }
                var carnivore = wagon.Animals.FirstOrDefault(a => a.Type == AnimalType.Carnivore);
                if (carnivore != null)
                {
                    if (animal.Size <= carnivore.Size) continue;
                }
                if ((int) animal.Size + wagon.GetTotalSize() > wagon.MaxSize) continue;
                foundWagon = true;
                wagon.Animals.Add(animal);
            }

            if (!foundWagon)
            {
                var wagon = new Wagon();
                wagon.Animals.Add(animal);
                Wagons.Add(wagon);
            }
            
        }
        Wagons.ForEach(Console.WriteLine);
    }
}