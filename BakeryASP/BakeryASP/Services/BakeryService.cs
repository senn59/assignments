namespace BakeryASP.Services;

public class BakeryService
{
    public Bakery.Core.Bakery Bakery { get; private set; } = new Bakery.Core.Bakery("test");
}