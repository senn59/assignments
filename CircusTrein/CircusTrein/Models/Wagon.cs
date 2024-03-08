namespace CircusTrein.Models;

public class Wagon
{
    //TODO: convert to ireadonlylist with fallback variable
    public List<Animal> Animals { get; set; } = new List<Animal>();
    
    public int MaxSize { get; private set; } = 10;


    //TODO: seperate into more functions 
    //TODO: Look into error codes or exceptions instead of returning a nullable string
    public string? TryFitAnimals(List<Animal> animals)
    {
        animals.AddRange(Animals);
        if (GetTotalSize() > MaxSize)
        {
            return $"Animals exceeded maxsize of {MaxSize}";
        }

        if (animals.Count < 2)
        {
            Animals = animals;
            return null;
        }

        for (int i = 0; i < animals.Count; i++)
        {
            for (int j = i + 1; j < animals.Count; j++)
            {
                if (!animals[i].IsCompatibleWith(animals[j]))
                {
                    return $"Animal ({animals[i]}) is not compatible with Animal ({animals[j]})";
                }
                
            }
        }

        Animals = animals;
        return null;
    }
    
    public int GetTotalSize()
    {
        int size = 0;
        foreach (var animal in Animals)
        {
            size += (int) animal.Size;
        }

        return size;
    }

    public override string ToString()
    {
        string str = "[ \n";
        foreach (var a in Animals)
        {
            str += $"   {a} \n";
        }

        str += "]";
        return str;
    }
}