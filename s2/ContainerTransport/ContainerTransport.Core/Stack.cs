namespace ContainerTransport.Core;

public class Stack
{
    private List<Container> _containers = new List<Container>();
    public IReadOnlyList<Container> Containers => _containers.AsReadOnly();
    public int Size => _containers.Count;
    public int Weight => _containers.Sum(c => (int)c.Load);
    public int X { get; init; }
    public int Y { get; init; }

    public void Add(Container container)
    {
        if (_containers.Count > 0 && container.Type is ContainerType.Valuable or ContainerType.CoolableValuable)
        {
            throw new Exception("Cant place container");
        }
        _containers.Add(container);
    }
    
    public void Reverse()
    {
        _containers.Reverse();
    }
}