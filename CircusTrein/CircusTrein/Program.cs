// See https://aka.ms/new-console-template for more information

using CircusTrein.Models;

namespace CircusTrein
{
    internal class Program
    {
        static void Main()
        {
            
            var animals = generateList(
                new AnimalSelection(1, 3, 2), 
                new AnimalSelection(0, 0, 3)
            );
            var train = new Train(animals);
            foreach (var w in train.Wagons)
            {
                Console.WriteLine(w);
            }
            Console.WriteLine($"Train size: {train.Size}");
        }
        
        static List<Animal> generateList(AnimalSelection carnivores, AnimalSelection herbivores)
        {
            var animals = new List<Animal>();
        
            animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Small, AnimalType.Carnivore), carnivores.Small));
            animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Medium, AnimalType.Carnivore), carnivores.Medium));
            animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Large, AnimalType.Carnivore), carnivores.Large));
            animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Small, AnimalType.Herbivore), herbivores.Small));
            animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Medium, AnimalType.Herbivore), herbivores.Medium));
            animals.AddRange(Enumerable.Repeat(new Animal(AnimalSize.Large, AnimalType.Herbivore), herbivores.Large));

            return animals;
        }

        public class AnimalSelection(int small, int medium, int large)
        {
            public int Small = small;
            public int Medium = medium;
            public int Large = large;
        }
    }
    
}