namespace BakeryASP.Services;
using Bakery.Core;

public class BakeryService
{
    public Bakery Bakery { get; private set; } = new Bakery("test");
}