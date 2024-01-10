using Bakery.Core.Files;

namespace Bakery.Core;

public class Bakery
{
    public string Name { get; private set; }
    private List<Sandwich> _sandwiches;
    private SandwichFiles _sandwichFiles;
    public IReadOnlyList<Ingredient> Ingredients = new List<Ingredient>([
        new Ingredient("lettuce", 0.2),
        new Ingredient("tomato", 0.3),
        new Ingredient("ham", 0.25),
        new Ingredient("turkey", 0.15),
        new Ingredient("cheese", 0.2),
        new Ingredient("bacon", 0.5),
        new Ingredient("jam", 0.1)
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
}