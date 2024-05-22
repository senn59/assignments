namespace ContainerTransport.Core;

public class Stack
{
    private List<Container> _containers = new List<Container>();
    public IReadOnlyList<Container> Containers => _containers.AsReadOnly();

    public void Add(Container container)
    {
        _containers.Add(container);
    }
}