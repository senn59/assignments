using System.Data;
using Microsoft.VisualBasic.CompilerServices;

namespace CircusTrein.Models;

public class Wagon
{
    //TODO: convert to ireadonlylist with fallback variable

    private List<Animal> _animals = new List<Animal>();
    
    public IReadOnlyList<Animal> Animals => _animals;
    
    public int MaxSize { get; private set; } = 10;


    //TODO: Look into error codes or exceptions instead of returning a nullable string
    public void TryFitAnimal(Animal animal)
    {
        if (!IsCompatible(animal))
        {
            throw new IncompatibleAnimalException(
                $"Animal {animal} is not compatible with other animals in the wagon.");
        }
        
        if (GetTotalSize() + (int) animal.Size > MaxSize)
        {
            throw new ExceedsMaxWagonSizeException(
                $"Adding animal {animal} exceeds the maximum wagon size of {MaxSize}");
        }
        _animals.Add(animal);
    }

    private bool IsCompatible(Animal animalToAdd)
    {
        //TODO: fallback variable of public property gebruiken hier?
        foreach (var animal in _animals)
        {
            if (!animalToAdd.IsCompatibleWith(animal))
            {
                return false;
            }
        }

        return true;
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