namespace ContainerTransport.Core;

public class ContainerGenerator
{
    private ContainerType[] _types = Enum.GetValues<ContainerType>();
    private ContainerLoad[] _weights = Enum.GetValues<ContainerLoad>();
    private Random _random = new Random();
    public List<Container> GenerateRandomContainers(Ship ship)
    {
        var containers = new List<Container>();
        var randomWeight = GetRandomWeight(ship.MaxWeight);
        int maxCoolableValuable = _random.Next(ship.Width + 1);
        int maxValuable = Math.Abs(_random.Next(ship.Width * 2 + 1) - maxCoolableValuable);
        int maxCoolable = _random.Next(ship.Width * 3 + 1);
        
        containers.AddRange(Enumerable.Repeat(GenerateContainer(ContainerType.Coolable), maxCoolable));
        containers.AddRange(Enumerable.Repeat(GenerateContainer(ContainerType.Valuable), maxValuable));
        containers.AddRange(Enumerable.Repeat(GenerateContainer(ContainerType.CoolableValuable), maxCoolableValuable));
        
        int weight = containers.Sum(c => (int)c.Load);
        
        while (weight < randomWeight)
        {
            var container = GenerateContainer(ContainerType.Normal);
            containers.Add(container);
            weight += (int)container.Load;
        }
        return containers;
    }

    private int GetRandomWeight(int maxShipWeight)
    {
        var minimumWeight = maxShipWeight / 2 + 4;
        return _random.Next(minimumWeight, maxShipWeight);
    }

    private Container GenerateContainer(ContainerType type)
    {
        var randomWeight = _weights[_random.Next(_weights.Length)];
        return new Container(type, randomWeight);
    }

    
}