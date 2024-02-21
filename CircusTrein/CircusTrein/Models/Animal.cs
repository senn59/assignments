namespace CircusTrein.Models;

enum AnimalType
{
    Herbivore,
    Carnivore
}

public class Animal
{
    public string Name { get; private init; }
    public int Size { get; private init; }
    public AnimalType Type { get; private init; }

    public Animal(string name, int size, AnimalType type)
    {
        Name = name;
        Size = size;
        Type = type;
    }
}