namespace CircusTrein.Models;

public class AnimalCollection
{
    public Animal Animal { get; private init; }
    public int Count { get; set; }

    public AnimalCollection(Animal animal, int count)
    {
        Animal = animal;
        Count = count;
    }
}