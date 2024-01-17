using Bakery.Core.Files;

namespace Bakery.Core;

public class Bakery
{
    public string Name { get; private set; }
    private List<Sandwich> _sandwiches;
    private SandwichFiles _sandwichFiles;
    public IReadOnlyList<Ingredient> Ingredients = new List<Ingredient>([
        new Ingredient("lettuce", 0.2m),
        new Ingredient("tomato", 0.3m),
        new Ingredient("ham", 0.25m),
        new Ingredient("turkey", 0.15m),
        new Ingredient("cheese", 0.2m),
        new Ingredient("bacon", 0.5m),
        new Ingredient("jam", 0.1m)
    ]).AsReadOnly();

    public Bakery(string name)
    {
        this.Name = name;
        _sandwichFiles = new SandwichFiles(this, "sandwiches.csv");
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
}