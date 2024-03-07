using CircusTrein.Models;

namespace CircusTrein.Tests;

public class Tests
{
    [Test]
    public void Scenario1()
    {
        var animals = generateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(0, 3, 2)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario2()
    {
        var animals = generateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(5, 2, 1)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario3()
    {
        var animals = generateList(
            new AnimalSelection(1, 1, 1), 
            new AnimalSelection(1, 1, 1)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(4));
    }
    
    [Test]
    public void Scenario4()
    {
        var animals = generateList(
            new AnimalSelection(1, 1, 2), 
            new AnimalSelection(1, 5, 1)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(5));
    }
    
    [Test]
    public void Scenario5()
    {
        var animals = generateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(1, 1, 2)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }

    [Test]
    public void Scenario6()
    {
        var animals = generateList(
            new AnimalSelection(3, 0, 0), 
            new AnimalSelection(0, 2, 3)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(3));
    }
    
    [Test]
    public void Scenario7()
    {
        var animals = generateList(
            new AnimalSelection(7, 3, 3), 
            new AnimalSelection(0, 5, 6)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(13));
    }
    
    [Test]
    public void Scenario8()
    {
        var animals = generateList(
            new AnimalSelection(0, 0, 0), 
            new AnimalSelection(5, 3, 1)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario9()
    {
        var animals = generateList(
            new AnimalSelection(1, 3, 2), 
            new AnimalSelection(0, 0, 3)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(6));
    }
    
    [Test]
    public void Scenario10()
    {
        var animals = generateList(
            new AnimalSelection(2, 2, 2), 
            new AnimalSelection(5, 5, 5)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(8));
    }
    
    [Test]
    public void Scenario11()
    {
        var animals = generateList(
            new AnimalSelection(0, 0, 0), 
            new AnimalSelection(1, 3, 2)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario12()
    {
        var animals = generateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(0, 3, 2)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario13()
    {
        var animals = generateList(
            new AnimalSelection(2, 0, 0), 
            new AnimalSelection(0, 2, 2)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario14()
    {
        var animals = generateList(
            new AnimalSelection(2, 0, 0), 
            new AnimalSelection(0, 6, 2)
            );
        var train = new Train(animals);
        Assert.That(train.Size, Is.EqualTo(3));
    }
    
    public List<Animal> generateList(AnimalSelection carnivores, AnimalSelection herbivores)
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

public class AnimalSelection(int small, int medium, int large)
{
    public int Small = small;
    public int Medium = medium;
    public int Large = large;
}
