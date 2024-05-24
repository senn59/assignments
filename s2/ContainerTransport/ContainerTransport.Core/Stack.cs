namespace ContainerTransport.Core;

public class Stack
{
    private List<Container> _containers = new List<Container>();
    public IReadOnlyList<Container> Containers => _containers.AsReadOnly();
    public int Size => _containers.Count;
    public int Weight => _containers.Sum(c => (int)c.Load);

    public void Add(Container container)
    {
        // if (_containers.Count > 0 && container.Type is ContainerType.Valuable or ContainerType.CoolableValuable)
        // {
        //     throw new Exception("Cant place container");
        // }
        if (container.Type is ContainerType.Valuable or ContainerType.CoolableValuable || !HasValuableContainer())
        {
            _containers.Add(container);
            return;
        }
        _containers.Insert(_containers.Count - 1, container);
    }

    public bool HasValuableContainer()
    {
        return _containers.Any(c => c.Type is ContainerType.CoolableValuable or ContainerType.Valuable);   
    }

    public bool IsLightEnoughFor(Container container)
    {
        return  _containers.Skip(1).Sum(c => (int)c.Load) + (int)container.Load < 120;
    }
}