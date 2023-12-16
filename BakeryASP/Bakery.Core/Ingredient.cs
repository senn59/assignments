namespace Bakery.Core;

public class Ingredient
{
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Ingredient(string name, double price)
    {
        this.Name = name;
        this.Price = price;
    }
}