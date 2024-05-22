namespace ContainerTransport.Core;

public class Container
{
    public ContainerType Type { get; private init; }
    public int Weight { get; private init; }

    public Container(ContainerType type, int weight)
    {
        Type = type;
        Weight = weight;
    }
}