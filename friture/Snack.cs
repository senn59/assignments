namespace friture;

public class Snack(string name, decimal price, int amountInStock)
{
    public string Name { get; private set; } = name;
    private decimal Price { get; set; } = price; //property vs class field when private?
    public int AmountInStock { get; private set; } = amountInStock;

    public SnackResult CanOrder(int amount)
    {
        if (AmountInStock < amount)
        {
            return new SnackResult(false, "not enough snacks in stock");
        }
        return new SnackResult(ok: true);
    }

    public decimal Order(int amount)
    {
        this.AmountInStock -= amount;
        return this.Price * amount;
    }
}