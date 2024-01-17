namespace Bakery.Core;

public class Ingredient
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public Ingredient(string name, decimal price)
    {
        this.Name = name;
        this.Price = price;
    }
}