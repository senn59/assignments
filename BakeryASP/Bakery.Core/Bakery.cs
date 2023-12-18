using System.Runtime.CompilerServices;
using Bakery.Core.Files;

namespace Bakery.Core;

public class Bakery
{
    private const int VatPercentage = 9;
    public string Name { get; private set; }
    private double _revenue = 0;
    private List<Sandwich> _sandwiches;
    private SandwichFiles _sandwichFiles;
    public IReadOnlyList<Ingredient> Ingredients = new List<Ingredient>([
        new Ingredient("lettuce", 0.2),
        new Ingredient("tomato", 0.3),
        new Ingredient("ham", 0.25),
    ]).AsReadOnly();

    public Bakery(string name)
    {
        this.Name = name;
        _sandwichFiles = new SandwichFiles(this, "/home/senna/dl/sandwiches.csv");
        _sandwiches = _sandwichFiles.Load();
    }

    public void AddSandwich(Sandwich sandwich)
    {
        _sandwiches.Add(sandwich);
        _sandwichFiles.Save(_sandwiches);
    }

    public IReadOnlyList<Sandwich> GetAvailableSandwiches()
    {
        return _sandwiches.AsReadOnly();
    }
    
    public IReadOnlyList<Sandwich> GetAvailableSandwiches(BreadType bread)
    {
        var filteredSandwiches = _sandwiches.Where(s => s.Bread == bread).ToList();
        return filteredSandwiches.AsReadOnly();
    }

    public double SellSandwich(Sandwich sandwich)
    {
        var price = sandwich.GetPrice();
        _revenue += price;
        return CalculateTotalWithVat(price);
    }

    public double CalculateRevenue(bool includeVat)
    {
        return includeVat ? CalculateTotalWithVat(_revenue) : _revenue;
    }

    private double CalculateTotalWithVat(double total)
    {
        return total + total * (VatPercentage / 100.0);
    }
}