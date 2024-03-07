namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    public int Size => Wagons.Count;
    
    private List<Animal> Herbivores { get; set; }
    private List<Animal> Carnivores { get; set; }
    private List<Animal> _carnivoresToRemove = new List<Animal>();
    private List<Animal> _herbivoresToRemove = new List<Animal>();
    

    public Train(List<Animal> animals)
    {
        Herbivores = animals.Where(a => a.Type == AnimalType.Herbivore).ToList();
        Herbivores = Herbivores.OrderBy(a => a.Size).ToList();
        Carnivores = animals.Where(a => a.Type == AnimalType.Carnivore).ToList();
        Carnivores = Carnivores.OrderByDescending(a => a.Size).ToList();
        CreateWagon();
    }
    
    private void CreateWagon()
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
                        Console.WriteLine(res);
                        return;
                    }
                    Wagons.Add(wagon);
                } break;
                case AnimalSize.Medium:
                {
                    Wagon wagon = new Wagon();
                    var herb = Herbivores.FirstOrDefault(c => c.Size == AnimalSize.Large);
                    string? res = null;
                    if (herb != null)
                    {
                        res = wagon.TryFitAnimals([carni, herb]);
                        _herbivoresToRemove.Add(herb);
                    }
                    else
                    {
                        res = wagon.TryFitAnimals([carni]);
                        _carnivoresToRemove.Add(carni);
                    }

                    if (res != null)
                    {
                        Console.WriteLine(res);
                        return;
                    }
                    Wagons.Add(wagon);
                } break;
                case AnimalSize.Small:
                {
                    Wagon wagon = new Wagon();
                    wagon.TryFitAnimals([carni]);
                    _carnivoresToRemove.Add(carni);
                    
                    var herbs = Herbivores.Where(a =>  (int) a.Size > (int) AnimalSize.Small);
                    // herbs = herbs.OrderByDescending(a => a.Size);
                    // herbs = herbs.OrderBy(a => a.Size);
                    foreach (var herb in herbs)
                    {
                        if (wagon.TotalSize(wagon.Animals) + (int) herb.Size > wagon.MaxSize) continue;
                        var res = wagon.TryFitAnimals([herb]);
                        if (res != null)
                        {
                            Console.WriteLine(res);
                            return;
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
            return;
        }

        var wagonBuffer = new Wagon();
        foreach (var herb in Herbivores)
        {
            if (wagonBuffer.TotalSize(wagonBuffer.Animals) + (int) herb.Size <= wagonBuffer.MaxSize)
            {
                var res = wagonBuffer.TryFitAnimals([herb]);
                if (res != null)
                {
                    Console.WriteLine(res);
                    return;
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
    }

    private List<Animal> UpdateList(List<Animal> original, List<Animal> selectedAnimals)
    {
        var beforeCount = original.Count;
        foreach (var animal in selectedAnimals)
        {
            original.Remove(animal);
        }
        Console.WriteLine($"{beforeCount} - {selectedAnimals.Count} = {original.Count}");

        return original;
        // Console.WriteLine($"{original.Where(a => !selectedAnimals.Contains(a)).ToList().Count} after");

        // return original.Where(a => !selectedAnimals.Contains(a)).ToList();
    }
}