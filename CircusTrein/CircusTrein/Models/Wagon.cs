using System.Data;

namespace CircusTrein.Models;

public class Wagon
{
    //TODO: convert to ireadonlylist with fallback variable

    private List<Animal> _animals = new List<Animal>();
    
    public IReadOnlyList<Animal> Animals => _animals;
    
    public int MaxSize { get; private set; } = 10;


    //TODO: Look into error codes or exceptions instead of returning a nullable string
    public string? TryFitAnimal(Animal animal)
    {
        if (GetTotalSize() + (int) animal.Size > MaxSize)
        {
            return $"Adding animal {animal} exceeds the maximum wagon size of {MaxSize}";
        }

        var res = CheckCompatiblity(animal);
        if (res != null)
        {
            return res;
        }
        
        _animals.Add(animal);
        return null;
    }

    private string? CheckCompatiblity(Animal animalToAdd)
    {
        //TODO: fallback variable of public property gebruiken hier?
        foreach (var animal in _animals)
        {
            if (!animalToAdd.IsCompatibleWith(animal))
            {
                return $"Animal ({animal}) is not compatible with Animal ({animalToAdd})";
            }
            
        }
        return null;
    }
    
    public int GetTotalSize()
    {
        return _animals.Sum(animal => (int)animal.Size);
    }

    public override string ToString()
    {
        var str = "[ \n";
        foreach (var a in _animals)
        {
            str += $"\t{a}\n";
        }
        str += "]\n";
        return str;
    }
}