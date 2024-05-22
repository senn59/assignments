namespace friture.Models;

public class Snack(string name, decimal price, int amountInStock)
{
    public string Name { get; private set; } = name;
    private decimal Price { get; set; } = price; //property vs class field when private?
    public int AmountInStock { get; private set; } = amountInStock;

    public SnackResult CanOrder(int amount)
    {
        if (AmountInStock < amount)
        {
            return new SnackResult(false, $"not enough \"{Name}\" in stock ({AmountInStock})");
        }
        return new SnackResult(ok: true);
    }

    public decimal Order(int amount)
    {
        this.AmountInStock -= amount;
        return this.Price * amount;
    }
}