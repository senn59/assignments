namespace Bakery.Core;

public class Sandwich
{
    public string Name { get; private set; }
    public decimal BasePrice { get; private set; }
    public BreadType Bread { get; private set; }
    private List<Ingredient> _ingredients = new List<Ingredient>();
    public IReadOnlyList<Ingredient> Ingredients => _ingredients.AsReadOnly();
    private const int MaxIngredients = 5;

    public Sandwich(string name, BreadType bread, decimal basePrice)
    {
        this.Name = name;
        this.Bread = bread;
        this.BasePrice = basePrice;
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
        var ingredientNames = _ingredients.Select(i => i.Name);
        var ingredients = string.Join(" ", ingredientNames);
        return $"{Name} ({Bread} with {ingredients})";
    }

    public decimal GetPrice()
    {
        var price = BasePrice;
        foreach (var ingredient in _ingredients)
        {
            price += ingredient.Price;
        }
        return price;
    }

    public override string ToString()
    {
        var str = $"{Name} | {this.BasePrice} {this.GetPrice()} | ";
        foreach (var i in _ingredients)
        {
            str += $"{i.Name} ";
        }
        return str;
    }
}