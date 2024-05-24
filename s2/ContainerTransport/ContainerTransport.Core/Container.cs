namespace ContainerTransport.Core;

public class Container
{
    public ContainerType Type { get; private init; }
    public ContainerLoad Load { get; private init; }

    public Container(ContainerType type, ContainerLoad load)
    {
        Type = type;
        Load = load;
    }

    public override string ToString()
    {
        return $"{Type}, {Load}";
    }
}