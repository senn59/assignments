namespace friture;

public class SnackBar
{
    private readonly List<Snack> snackList;
    public decimal TotalRevenue;
    public IReadOnlyList<Snack> SnackList => snackList.AsReadOnly();
    public SnackBar(List<Snack> snackList)
    {
        this.snackList = snackList;
    }
    public decimal ProcessOrder()
    {
        //TODO: implement
        return 0.0m;
    }
    
    public List<string> GetSnackNames()
    {
        return snackList.Select(snack => snack.Name).ToList();
    }

    public List<(string, int)> GetStock()
    {
        var stockPerItem = new List<(string, int)>();
        foreach (var snack in snackList)
        {
            stockPerItem.Add((snack.Name, snack.AmountInStock));
        }
        return stockPerItem;
    }
}