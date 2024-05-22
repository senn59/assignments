using CircusTrein.Models;
using CircusTrein.Tests.utilities;

namespace CircusTrein.Tests;

public class AcceptanceTests
{
    [Test]
    public void Scenario1()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(0, 3, 2)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario2()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(5, 2, 1)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario3()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 1, 1), 
            new AnimalSelection(1, 1, 1)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(4));
    }
    
    [Test]
    public void Scenario4()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 1, 2), 
            new AnimalSelection(1, 5, 1)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(5));
    }
    
    [Test]
    public void Scenario5()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(1, 1, 2)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }

    [Test]
    public void Scenario6()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(3, 0, 0), 
            new AnimalSelection(0, 2, 3)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(3));
    }
    
    [Test]
    public void Scenario7()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(7, 3, 3), 
            new AnimalSelection(0, 5, 6)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(13));
    }
    
    [Test]
    public void Scenario8()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(0, 0, 0), 
            new AnimalSelection(5, 3, 1)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario9()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 3, 2), 
            new AnimalSelection(0, 0, 3)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(6));
    }
    
    [Test]
    public void Scenario10()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(2, 2, 2), 
            new AnimalSelection(5, 5, 5)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(8));
    }
    
    [Test]
    public void Scenario11()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(0, 0, 0), 
            new AnimalSelection(1, 3, 2)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario12()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(1, 0, 0), 
            new AnimalSelection(0, 3, 2)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario13()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(2, 0, 0), 
            new AnimalSelection(0, 2, 2)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(2));
    }
    
    [Test]
    public void Scenario14()
    {
        var animals = AnimalListGenerator.GenerateList(
            new AnimalSelection(2, 0, 0), 
            new AnimalSelection(0, 6, 2)
            );
        var train = new Train(animals);
        foreach (var w in train.Wagons) Console.WriteLine(w);
        Assert.That(train.Size, Is.EqualTo(3));
    }
    
}

