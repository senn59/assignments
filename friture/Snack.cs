namespace friture;

public class Snack
{
    public string Name { get; private set; }
    private decimal Price { get; set; } //property vs class field when private?
    public int AmountInStock { get; private set; }
    public Snack(string name, decimal price, int amountInStock)
    {
        this.Name = name;
        this.Price = price;
        this.AmountInStock = amountInStock;
    }

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
/*
 how to see u need canorder for order?
*/