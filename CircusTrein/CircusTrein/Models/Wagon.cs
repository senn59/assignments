namespace CircusTrein.Models;

public class Wagon
{
    private const int MaxSize = 10;
    public List<Animal> Animals { get; private set; } = new List<Animal>();

    public string? TryFitAnimals(List<Animal> animals)
    {
        if (TotalSize(animals) > MaxSize)
        {
            return $"Animals exceeded maxsize of {MaxSize}";
        }

        int carnivoreCount = animals.FindAll(a => a.Type == AnimalType.Carnivore).Count;
        if (carnivoreCount > 1)
        {
            return $"Too many carnivores for 1 wagon";
        }
        
        int? carnivoreSize = animals.FirstOrDefault(a => a.Type == AnimalType.Carnivore).Size;
        foreach (var animal in animals)
        {
            if (animal.Type == AnimalType.Herbivore && animal.Size <= carnivoreSize)
            {
                return $"Carnivore of size {carnivoreSize} is going to eat the herbivore of size {animal.Size}";
            }
        }

        Animals = animals;
        return null;
    }

    public string? TryFitAnimals(Animal animal)
    {
        if (Animals.Count > 0)
        {
            return $"There is already an animal in the wagon, use a list if adding multiple animals";
        }
        Animals.Add(animal);
        return null;
    }

    public int TotalSize(List<Animal> animals)
    {
        int size = 0;
        foreach (var animal in animals)
        {
            size += animal.Size;
        }

        return size;
    }
}