using CircusTrein.Models;
namespace CircusTrein.Tests.utilities;

public static class AnimalListGenerator
{
    public static List<Animal> GenerateList(AnimalSelection carnivores, AnimalSelection herbivores)
    {
        var animals = new List<Animal>();
        
        animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Small, AnimalType.Carnivore), carnivores.Small));
        animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Medium, AnimalType.Carnivore), carnivores.Medium));
        animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Large, AnimalType.Carnivore), carnivores.Large));
        animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Small, AnimalType.Herbivore), herbivores.Small));
        animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Medium, AnimalType.Herbivore), herbivores.Medium));
        animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Large, AnimalType.Herbivore), herbivores.Large));

        return animals;
    }
}
