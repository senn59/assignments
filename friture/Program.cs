using friture;

var snacks = new List<Snack>();
snacks.Add(new Snack("Frikandel", 1.50m, 20));
snacks.Add(new Snack("Kipnuggets", 1.20m, 25));
snacks.Add(new Snack("Croket", 1.80m, 24));
var snackBar = new SnackBar(snacks);

//OPTIONS 
const int order = 1;
const int revenue = 2;
const int stock = 3;

while (true) {
    Console.Clear();
    Console.WriteLine("What would you like to do? \n 1: Order \n 2: Check revenue \n 3: Check stock"); //aanpassing op meerdere plekken

    var option = Console.ReadLine();
    if (option == null) continue;

    if (!int.TryParse(option, out var parsedOption)) continue;

    switch (parsedOption)
    {
        case order:
            var clientOrder = new List<(Snack, int)>();

            foreach (var snack in snackBar.SnackList)
            {
                int amount = PromptAmount(snack);
                clientOrder.Add((snack, amount));
            }
            //var priceToPay = snackBar.ProcessOrder(clientOrder);
            //Console.WriteLine($"Your total is: ${priceToPay}");
            break;
        case revenue:
            Console.WriteLine(snackBar.TotalRevenue);
            break;
        case stock:
            var snackStock = snackBar.GetStock();
            foreach (var snack in snackStock)
            {
                Console.WriteLine(snack);
            }
            break;
        default:
            Console.WriteLine("Invalid option.");
            return;
    }
    Console.WriteLine("(Press enter to quit.)");
    Console.ReadLine();
}

int PromptAmount(Snack snack)
{
    Console.WriteLine($"How many of {snack} would you like to order? ({snack.AmountInStock} in stock)");
    var amount = Console.ReadLine();
    if (!int.TryParse(amount, out int parsedAmount))
    {
        PromptAmount(snack);
    }

    var result = snack.CanOrder(parsedAmount);
    if (!result.Ok)
    {
        Console.WriteLine(result.Message);
        PromptAmount(snack);
    }

    return parsedAmount;
}