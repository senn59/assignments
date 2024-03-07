namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    public int Size => Wagons.Count;
    
    private List<Animal> Herbivores { get; set; }
    private List<Animal> Carnivores { get; set; }
    private List<Animal> _herbivoresToRemove = new List<Animal>();
    private List<Animal> _carnivoresToRemove = new List<Animal>();
    

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
        LoadAnimals();
    }
    
    private void LoadAnimals()
    {
        foreach (var carni in Carnivores)
        {
            var wagon = new Wagon();
            wagon.TryFitAnimals([carni]);
            _carnivoresToRemove.Add(carni);
            var availableHerbivores = Herbivores.Where(a => (int)a.Size > (int)carni.Size).ToList();
            foreach (var herb in availableHerbivores)
            {
                if (wagon.TotalSize() + (int)herb.Size <= wagon.MaxSize)
                {
                    wagon.TryFitAnimals([herb]);
                    _herbivoresToRemove.Add(herb);
                }
            }
            Wagons.Add(wagon);
        }

        Herbivores = UpdateList(Herbivores, _herbivoresToRemove);
        var wagonBuffer = new Wagon();
        foreach (var herb in Herbivores)
        {
            if (wagonBuffer.TotalSize() + (int)herb.Size <= wagonBuffer.MaxSize)
            {
                var res = wagonBuffer.TryFitAnimals([herb]);
                if (res != null)
                {
                    Console.WriteLine(res);
                }
            }
            else
            {
                Console.WriteLine("wagon full");
                Console.WriteLine(wagonBuffer);
                Console.WriteLine(herb);
                
                Wagons.Add(wagonBuffer);
                wagonBuffer = new Wagon();
            }
        }

        if (wagonBuffer.TotalSize() > 0)
        {
            Wagons.Add(wagonBuffer);
        }

        Console.WriteLine("Final result:");
        Wagons.ForEach(Console.WriteLine);
    }

    private List<Animal> UpdateList(List<Animal> original, List<Animal> selectedAnimals)
    {
        foreach (var animal in selectedAnimals)
        {
            original.Remove(animal);
        }

        return original;
        // Console.WriteLine($"{original.Where(a => !selectedAnimals.Contains(a)).ToList().Count} after");

        // return original.Where(a => !selectedAnimals.Contains(a)).ToList();
    }
}