namespace friture;

public class SnackBar(List<Snack> list)
{
    public decimal TotalRevenue;
    public IReadOnlyList<Snack> SnackList => list.AsReadOnly();

    public decimal ProcessOrder(List<(Snack, int)> order)
    {
        decimal amountDue = 0;
        foreach (var (snack, amount) in order)
        {
            amountDue += snack.Order(amount);
        }
        
        TotalRevenue += amountDue;
        return amountDue;
    }
}