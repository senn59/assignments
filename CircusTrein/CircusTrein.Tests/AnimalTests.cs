using CircusTrein.Models;

namespace CircusTrein.Tests;

public class AnimalTests
{
    [Test]
    public void TwoCarnivores()
    {
        var smallCarni = new Animal(AnimalSize.Small, AnimalType.Carnivore);
        var mediumCarni = new Animal(AnimalSize.Medium, AnimalType.Carnivore);
        Assert.That(smallCarni.IsCompatibleWith(mediumCarni), Is.False);
    }
    
    [Test]
    public void TwoHerbivores()
    {
        var largeHerbi = new Animal(AnimalSize.Large, AnimalType.Herbivore);
        var mediumHerbi = new Animal(AnimalSize.Medium, AnimalType.Herbivore);
        Assert.That(largeHerbi.IsCompatibleWith(mediumHerbi), Is.True);
    }
    
    [Test]
    public void CarnivoreAndHerbivoreOfEqualSize()
    {
        var mediumCarni = new Animal(AnimalSize.Medium, AnimalType.Carnivore);
        var mediumHerbi = new Animal(AnimalSize.Medium, AnimalType.Herbivore);
        Assert.That(mediumCarni.IsCompatibleWith(mediumHerbi), Is.False);
    }
    
    [Test]
    public void SmallerHerbivoreWithLargerCarnivore()
    {
        var largeCarni = new Animal(AnimalSize.Large, AnimalType.Carnivore);
        var mediumHerbi = new Animal(AnimalSize.Medium, AnimalType.Herbivore);
        Assert.That(largeCarni.IsCompatibleWith(mediumHerbi), Is.False);
    }
    
    [Test]
    public void LargerHerbivoreWithSmallerCarnivore()
    {
        var mediumCarni = new Animal(AnimalSize.Medium, AnimalType.Carnivore);
        var largeHerbi = new Animal(AnimalSize.Large, AnimalType.Herbivore);
        Assert.That(mediumCarni.IsCompatibleWith(largeHerbi), Is.True);
    }
}