using friture.Models;

var snackBar = new SnackBar([
    new Snack("Frikandel", 1.50m, 20),
    new Snack("Kipnuggets", 1.20m, 25),
    new Snack("Croket", 1.80m, 24)
]);

var options = new List<Option>
{
    new Option("order", () =>
    {
        var clientOrder = new List<(Snack, int)>();

        foreach (var snack in snackBar.SnackList)
        {
            var amount = PromptOrderAmount(snack);
            clientOrder.Add((snack, amount));
        }
        var amountDue = snackBar.ProcessOrder(clientOrder);
        Console.WriteLine($"Your total is: ${amountDue}");
    }),
    new Option("check revenue", () =>
    {
        Console.WriteLine(snackBar.TotalRevenue);
    }, true),
    new Option("check stock", () =>
    {
        foreach (var snack in snackBar.SnackList)
        {
            Console.WriteLine($"{snack.Name}: {snack.AmountInStock}");
        }
    }, true)
};

while (true) {
    Console.Clear();
    Console.WriteLine("What would you like to do?");
    DisplayOptions(options);

    var option = Console.ReadLine();
    if (option == null) continue;

    if (!int.TryParse(option, out var parsedOption) || parsedOption < 1 || parsedOption > options.Count)
    {
        continue;
    }
    // -1 because list index starts at 0 but in the UI we increment by 1 for aesthetics
    options[parsedOption - 1].Callback();
    
    Console.WriteLine("(Press any key to quit.)");
    Console.ReadLine();
}

int PromptOrderAmount(Snack snack)
{
    Console.WriteLine($"How many of {snack.Name} would you like to order?");
    var amount = Console.ReadLine();
    if (!int.TryParse(amount, out var parsedAmount))
    {
        return PromptOrderAmount(snack);
    }
    
    var result = snack.CanOrder(parsedAmount);
    if (!result.Ok)
    {
        Console.WriteLine(result.Message);
        return PromptOrderAmount(snack);
    }
    
    return parsedAmount;
}

void DisplayOptions(List<Option> options)
{
    for (int i = 0; i < options.Count; i++)
    {
        Console.WriteLine($"{i + 1}: {options[i].Name}");
    }
}
