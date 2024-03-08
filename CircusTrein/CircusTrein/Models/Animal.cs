namespace CircusTrein.Models;

public class Animal
{
    public AnimalSize Size { get; private init; }
    public AnimalType Type { get; private init; }

    public Animal(AnimalSize size, AnimalType type)
    {
        Size = size;
        Type = type;
    }
    
    //TODO: check if this can be done more efficiently
    public bool IsCompatibleWith(Animal animal)
    {
        if (this.Type == AnimalType.Carnivore && animal.Type == AnimalType.Carnivore)
        {
            return false;
        }

        if (this.Type == animal.Type) return true;
        
        if (this.Type == AnimalType.Carnivore && this.Size >= animal.Size)
        {
            return false;
        }

        if (this.Type == AnimalType.Herbivore && this.Size <= animal.Size)
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return $"{Type}, {Size}";
    }
}