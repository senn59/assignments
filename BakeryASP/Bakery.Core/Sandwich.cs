namespace Bakery.Core;

public class Sandwich
{
    public string Name { get; private set; }
    private double _basePrice;
    public BreadType Bread { get; private set; }
    private List<Ingredient> _ingredients = new List<Ingredient>();
    private const int MaxIngredients = 5;

    public Sandwich(string name, BreadType bread, double basePrice)
    {
        this.Name = name;
        this.Bread = bread;
        this._basePrice = basePrice;
    }
    //TODO: load ingredient func when working with files?

    public string? AddIngredient(Ingredient ingredient)
    {
        if (_ingredients.Count == MaxIngredients)
        {
            return $"Maximum amount ingredients of {MaxIngredients} reached.";
        }
        _ingredients.Add(ingredient);
        return null;
    }

    public string GetInfo()
    {
        var ingredients = string.Join(" ", _ingredients);
        return $"{Name} ({Bread} with {ingredients})";
    }

    public double GetPrice()
    {
        var price = _basePrice;
        foreach (var ingredient in _ingredients)
        {
            price += ingredient.Price;
        }
        return price;
    }
}