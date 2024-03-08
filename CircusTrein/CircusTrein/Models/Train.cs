namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    public int Size => Wagons.Count;
    
    private List<Animal> _herbivores;
    private List<Animal> _carnivores;
    private List<Animal> _herbivoresToDelete = new List<Animal>();
    

    public Train(List<Animal> animals)
    {
        _herbivores = animals
            .Where(a => a.Type == AnimalType.Herbivore)
            .OrderBy(a => a.Size)
            .ToList();
        _carnivores = animals
            .Where(a => a.Type == AnimalType.Carnivore)
            .OrderBy(a => a.Size)
            .ToList();
        
        var mediumHerbs = _herbivores.Count(a => a.Size == AnimalSize.Medium);

        if (mediumHerbs < 3)
        {
            _herbivores = _herbivores.OrderByDescending(a => a.Size).ToList();
        }

        
        LoadAnimals();
    }
    
    private void LoadAnimals()
    {
        foreach (var carni in _carnivores)
        {
            var wagon = new Wagon();
            wagon.Animals.Add(carni);
            Wagons.Add(wagon);
        }
        
        var wagonBuffer = new Wagon();
        var wagonsToAdd = new List<Wagon>();
        foreach (var herb in _herbivores)
        {
            bool addedHerb = false;
            foreach (var wagon in Wagons)
            {
                if (wagon.Animals[0].Size < herb.Size && wagon.GetTotalSize() + (int) herb.Size <= wagon.MaxSize)
                {
                    wagon.Animals.Add(herb);
                    addedHerb = true;
                    break;
                }
            }
            
            if (addedHerb)
            {
                continue;
            }

            if (wagonBuffer.GetTotalSize() + (int)herb.Size <= wagonBuffer.MaxSize)
            {
                wagonBuffer.Animals.Add(herb);
            }
            else
            {
                wagonsToAdd.Add(wagonBuffer);
                wagonBuffer = new Wagon();
                wagonBuffer.Animals.Add(herb);
            }
        }
        if (wagonBuffer.Animals.Count > 0)
        {
            wagonsToAdd.Add(wagonBuffer);
        }

        if (wagonsToAdd.Count > 0)
        {
            Wagons.AddRange(wagonsToAdd);
        }
        Wagons.ForEach(Console.WriteLine);
    }
}