namespace Bakery.Core.Files;

public class SandwichFiles
{
    private Bakery _bakery;
    private string _path;

    //TODO: move tryparse and serialize to sandwich class?
    public SandwichFiles(Bakery bakery, string path)
    {
        this._bakery = bakery;
        this._path = path;
    }

    public List<Sandwich> Load()
    {
        var sandwiches = new List<Sandwich>();
        
        var lines = File.ReadAllLines(_path);
        foreach (var line in lines)
        {
            var columns = line.Split(",");
            var sandwich = TryParseSandwich(
                columns[0],
                columns[1],
                columns[2],
                columns[3].Split(";")
            );
            if (sandwich == null)
            {
                //TODO: error logging
                continue;
            }
            sandwiches.Add(sandwich);
        }
        return sandwiches;
    }
    
    public string? Save(List<Sandwich> sandwiches)
    {
        List<string> fileContents = new List<string>();
        foreach (var sandwich in sandwiches)
        {
            var serialized = SerializeSandwich(sandwich);
            fileContents.Add(serialized);
        }
        File.WriteAllLines(_path, fileContents);
        return null;
    }
    
    private Sandwich? TryParseSandwich(string name, string bread, string price, string[] ingredients)
    {
        if (!Enum.TryParse(bread, out BreadType breadType))
        {
            return null;
        }
        
        if (!decimal.TryParse(price, out var parsedPrice))
        {
            return null;
        }
        
        var sandwich = new Sandwich(name, breadType, parsedPrice);
        foreach (var ingredient in ingredients)
        {
            var ingredientObject = _bakery.Ingredients.FirstOrDefault(i => i.Name == ingredient);
            if (ingredientObject != null)
            {
                //TODO: error handling
                sandwich.AddIngredient(ingredientObject);
            }
        }
        return sandwich;
    }

    private string SerializeSandwich(Sandwich sandwich)
    {
        string ingredientsString = string.Empty;
        foreach (var ingredient in sandwich.Ingredients)
        {
            ingredientsString += $"{ingredient.Name};";
        }
        return $"{sandwich.Name},{sandwich.Bread},{sandwich.BasePrice},{ingredientsString}";
    }
}