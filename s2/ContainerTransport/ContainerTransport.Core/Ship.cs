namespace ContainerTransport.Core;

public class Ship
{
    private readonly Stack[,] _cargo;
    private readonly List<Container> _containers;
    
    public int Length { get; private init; }
    public int Width { get; private init; }
    public int MaxWeight { get; set; }
    public Stack[,] Cargo => (Stack[,])_cargo.Clone();

    public Ship(int width, int length, List<Container> containers)
    {
        Length = length;
        Width = width;
        MaxWeight = length * width * 150; //max stack size
        _cargo = new Stack[width, length];
        _containers = containers.OrderByDescending(c => c.Type).ToList();
        _containers.ForEach(PlaceContainer);
    }

    private void PlaceContainer(Container c)
    {
        switch (c.Type)
        {
            case ContainerType.Normal:
                PlaceNormalContainer(c);
                return;
            case ContainerType.Valuable:
                PlaceValuableContainer(c);
                return;
            case ContainerType.Coolable:
                PlaceCoolableContainer(c);
                return;
            case ContainerType.CoolableValuable:
                PlaceCoolableValueableContainer(c);
                return;
        }
    }

    private void PlaceNormalContainer(Container c)
    {
        
    }

    private void PlaceValuableContainer(Container c)
    {
        
    }

    private void PlaceCoolableContainer(Container c)
    {
        
    }

    private void PlaceCoolableValueableContainer(Container c)
    {
        var row = 0;
        for (var i = 0; i < _cargo.GetLength(1); i++)
        {
            var stack = _cargo[row, i];
            if (stack.Size != 0) continue;
            stack.Add(c);
            _containers.Remove(c);
        }
    }
}