namespace friture;

public class SnackBar
{
    private List<Snack> snackList;
    public decimal TotalRevenue;
    public IReadOnlyList<Snack> SnackList => snackList.AsReadOnly();
    
    public SnackBar(List<Snack> snackList)
    {
        this.snackList = snackList;
    }
    public decimal ProcessOrder(List<(Snack, int)> order)
    {
        decimal amountDue = 0;
        foreach (var (snack, amount) in order)
        {
            amountDue += snack.Order(amount);
        }
        return amountDue;
    }
}