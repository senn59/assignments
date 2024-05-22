using CircusTrein.Models;
using CircusTrein.Tests.utilities;

namespace CircusTrein.Tests;

public class WagonUnitTests
{
    [Test]
    public void IncompatibleAnimalsInWagon()
    {
        var largeCarnivore = new Animal(AnimalSize.Large, AnimalType.Carnivore);
        
        var wagon = new Wagon();
        wagon.TryFitAnimal(largeCarnivore);
        Assert.Throws<IncompatibleAnimalException>(() => wagon.TryFitAnimal(largeCarnivore));
    }
    
    [Test]
    public void TooManyAnimalsInWagon()
    {
        var animal = new Animal(AnimalSize.Large, AnimalType.Herbivore);
        var animalSize = (int)animal.Size;
        var wagon = new Wagon();

        int loopCount = (wagon.MaxSize / animalSize) + 1;

        for (int i = 0; i < loopCount; i++)
        {
            try
            {
                wagon.TryFitAnimal(animal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.That(e, Is.TypeOf<ExceedsMaxWagonSizeException>());
                return;
            }
        }
        Assert.Fail("Shouldve thrown \"ExceedsMaxWagonSizeException\" but instead fills the wagon like normal");
    }
}