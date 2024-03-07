namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    public int Size => Wagons.Count;
    
    private List<Animal> Herbivores { get; set; }
    private List<Animal> Carnivores { get; set; }
    private List<Animal> _herbivoresToRemove = new List<Animal>();
    

    public Train(List<Animal> animals)
    {
        Herbivores = animals.Where(a => a.Type == AnimalType.Herbivore).ToList();
        Herbivores = Herbivores.OrderBy(a => a.Size).ToList();
        Carnivores = animals.Where(a => a.Type == AnimalType.Carnivore).ToList();
        Carnivores = Carnivores.OrderByDescending(a => a.Size).ToList();
    }
    
    public string? LoadAnimals()
    {
        foreach (var carni in Carnivores)
        {
            Herbivores = UpdateList(Herbivores, _herbivoresToRemove);
            switch (carni.Size)
            {
                case AnimalSize.Large:
                {
                    Wagon wagon = new Wagon();
                    var res = wagon.TryFitAnimals([carni]);
                    if (res != null)
                    {
                        return res;
                    }
                    Wagons.Add(wagon);
                } break;
                case AnimalSize.Medium:
                {
                    Wagon wagon = new Wagon();
                    var herb = Herbivores.FirstOrDefault(c => c.Size == AnimalSize.Large);
                    List<Animal> toTryFit = herb == null ? [carni] : [carni, herb];
                    var res = wagon.TryFitAnimals(toTryFit);
                    if (res != null)
                    {
                        return res;
                    }
                    if (herb != null) _herbivoresToRemove.Add(herb);
                    Wagons.Add(wagon);
                } break;
                case AnimalSize.Small:
                {
                    Wagon wagon = new Wagon();
                    wagon.TryFitAnimals([carni]);
                    
                    var herbs = Herbivores.Where(a =>  (int) a.Size > (int) AnimalSize.Small);
                    foreach (var herb in herbs)
                    {
                        if (wagon.TotalSize(wagon.Animals) + (int) herb.Size > wagon.MaxSize) continue;
                        var res = wagon.TryFitAnimals([herb]);
                        if (res != null)
                        {
                            return res;
                        }
                        _herbivoresToRemove.Add(herb);
                    }
                    Wagons.Add(wagon);
                } break;
            }
        }
        
     
        Herbivores = UpdateList(Herbivores, _herbivoresToRemove);
        
        if (Herbivores.Count <= 0)
        {
            return null;
        }

        var wagonBuffer = new Wagon();
        foreach (var herb in Herbivores)
        {
            if (wagonBuffer.TotalSize(wagonBuffer.Animals) + (int) herb.Size <= wagonBuffer.MaxSize)
            {
                var res = wagonBuffer.TryFitAnimals([herb]);
                if (res != null)
                {
                    return res;
                }
            }
            else
            {
                Wagons.Add(wagonBuffer);
                wagonBuffer = new Wagon();
                wagonBuffer.TryFitAnimals([herb]);
            }
        }

        if (wagonBuffer.Animals.Count > 0)
        {
            Wagons.Add(wagonBuffer);
        }
        Wagons.ForEach(Console.WriteLine);
        return null;
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