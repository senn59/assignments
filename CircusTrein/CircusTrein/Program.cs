// See https://aka.ms/new-console-template for more information

using CircusTrein.Models;

namespace CircusTrein
{
    internal class Program
    {
        static void Main()
        {
            int small = 1;
            int medium = 3;
            int large = 5;
            List<Animal> animals = new List<Animal>()
            {
                new Animal("Lion", 3, AnimalType.Carnivore),
                new Animal("Giraffe", 5, AnimalType.Herbivore),
                new Animal("Elephant", 5, AnimalType.Herbivore),
                new Animal("Bunny", 1, AnimalType.Herbivore),
                new Animal("Dog", 1, AnimalType.Carnivore),
            };
            
            var circusAnimals = GenerateCircus(animals);
            var train = new Train(circusAnimals);
            Console.WriteLine(train.Size);
            
            List<AnimalCollection> GenerateCircus(List<Animal> animals)
            {
                Random random = new Random();
                List<AnimalCollection> circus = new List<AnimalCollection>();
                foreach (var animal in animals)
                {
                    int count = random.Next(1, 3);
                    circus.Add(new AnimalCollection(animal, count));
                }
                return circus;
            }
        }

    }
    
}