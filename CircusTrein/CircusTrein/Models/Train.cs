namespace CircusTrein.Models;

public class Train
{
    public List<Wagon> Wagons { get; private set; } = new List<Wagon>();
    private List<AnimalCollection> AnimalsToTransport { get; set; }
    public int Size => Wagons.Count;
    
    private IEnumerable<AnimalCollection> Herbivores { get; set; }
    private IEnumerable<AnimalCollection> Carnivores { get; set; }
    

    public Train(List<AnimalCollection> animals)
    {
        AnimalsToTransport = animals;
        Herbivores = animals.Where(a => a.Animal.Type == AnimalType.Herbivore);
        Herbivores = Herbivores.OrderByDescending(a => a.Animal.Size);
        Carnivores = animals.Where(a => a.Animal.Type == AnimalType.Carnivore);
        Carnivores = Carnivores.OrderByDescending(a => a.Animal.Size);
        CreateWagon();
    }
    private void CreateWagon()
    {

        List<Animal> AnimalBuffer = new List<Animal>();
        foreach (var a in Carnivores)
        {
            switch (a.Animal.Size)
            {
                case 5:
                {
                    Wagon wagon = new Wagon();
                    var res = wagon.TryFitAnimals(a.Animal);
                    if (res != null)
                    {
                        Console.WriteLine(res);
                        return;
                    }
                    Wagons.Add(wagon);
                } break;
                case 3:
                {
                    Wagon wagon = new Wagon();
                    var herb = Herbivores.FirstOrDefault(a => a.Animal.Size == 5);
                    if (herb != null)
                    {
                        wagon.TryFitAnimals(new List<Animal>() { a.Animal, herb.Animal });
                    }
                } break;
                
            }
        }
    }
}