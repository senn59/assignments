namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    public int Size => Wagons.Count;
    
    private List<Animal> Herbivores { get; set; }
    private List<Animal> Carnivores { get; set; }
    private List<Animal> _herbivoresToRemove = new List<Animal>();
    private List<Animal> _carnivoresToRemove = new List<Animal>();
    private List<Animal> Animals { get; set; } = new List<Animal>();
    

    public Train(List<Animal> animals)
    {
        Herbivores = animals
            .Where(a => a.Type == AnimalType.Herbivore)
            .OrderBy(a => a.Size)
            .ToList();
        Carnivores = animals
            .Where(a => a.Type == AnimalType.Carnivore)
            .OrderBy(a => a.Size)
            .ToList();
        // var largeHerbs = Herbivores.Count(a => a.Size == AnimalSize.Large);
        var mediumHerbs = Herbivores.Count(a => a.Size == AnimalSize.Medium);
        var smallCarnis = Carnivores.Count(a => a.Size == AnimalSize.Small);

        if (mediumHerbs != 0 && smallCarnis != 0)
        {
            if (mediumHerbs % 3 != 0 && smallCarnis >= mediumHerbs)
            {
                Herbivores = Herbivores.OrderByDescending(a => a.Size).ToList();
            }
        }
        
        LoadAnimals();
    }
    
    private void LoadAnimals()
    {
        foreach (var carni in Carnivores)
        {
            var wagon = new Wagon();
            wagon.Animals.Add(carni);
            Wagons.Add(wagon);
        }

        var carnivoreWagons = Wagons.Count;
        
        
        var wagonBuffer = new Wagon();
        var wagonsToAdd = new List<Wagon>();
        foreach (var herb in Herbivores)
        {
            bool addedHerb = false;
            foreach (var wagon in Wagons)
            {
                if (wagon.Animals[0].Size < herb.Size && wagon.TotalSize() + (int) herb.Size <= wagon.MaxSize)
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

            if (wagonBuffer.TotalSize() + (int)herb.Size <= wagonBuffer.MaxSize)
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
        Console.WriteLine("Final result:");
        Wagons.ForEach(Console.WriteLine);
    }
}