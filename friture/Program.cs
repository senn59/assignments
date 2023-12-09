using friture;

var snacks = new List<Snack>
{
    new Snack("Frikandel", 1.50m, 20),
    new Snack("Kipnuggets", 1.20m, 25),
    new Snack("Croket", 1.80m, 24)
};
var snackBar = new SnackBar(snacks);

//OPTIONS 
const int order = 1;
const int revenue = 2;
const int stock = 3;

while (true) {
    Console.Clear();
    Console.WriteLine("What would you like to do? \n 1: Order \n 2: Check revenue \n 3: Check stock");

    var option = Console.ReadLine();
    if (option == null) continue;

    if (!int.TryParse(option, out var parsedOption)) continue;

    switch (parsedOption)
    {
        case order:
            var clientOrder = new List<(Snack, int)>();

            foreach (var snack in snackBar.SnackList)
            {
                var amount = PromptOrderAmount(snack);
                clientOrder.Add((snack, amount));
            }
            var amountDue = snackBar.ProcessOrder(clientOrder);
            Console.WriteLine($"Your total is: ${amountDue}");
            break;
        case revenue:
            Console.WriteLine(snackBar.TotalRevenue);
            break;
        case stock:
            foreach (var snack in snackBar.SnackList)
            {
                Console.WriteLine($"{snack.Name}: {snack.AmountInStock}");
            }
            break;
        default:
            Console.WriteLine("Invalid option.");
            return;
    }
    Console.WriteLine("(Press enter to quit.)");
    Console.ReadLine();
}

int PromptOrderAmount(Snack snack)
{
    Console.WriteLine($"How many of {snack.Name} would you like to order? ({snack.AmountInStock} in stock)");
    var amount = Console.ReadLine();
    if (!int.TryParse(amount, out var parsedAmount))
    {
        PromptOrderAmount(snack);
    }

    var result = snack.CanOrder(parsedAmount);
    if (!result.Ok)
    {
        Console.WriteLine(result.Message);
        PromptOrderAmount(snack);
    }

    return parsedAmount;
}