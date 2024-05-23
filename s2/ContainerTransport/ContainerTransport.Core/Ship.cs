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
        FillArray();
        _containers = containers.OrderByDescending(c => c.Type).ToList();
        _containers.ForEach(PlaceContainer);
        ReverseStacks();
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
        for (var i = 0; i < _cargo.GetLength(0); i++)
        {
            for (var j = 0; j < _cargo.GetLength(1); j++)
            {
                var stack = _cargo[i, j];
                if (stack.Weight < 150)
                {
                    stack.Add(c);
                    return;
                }
            }
        }
    }

    private void PlaceValuableContainer(Container c)
    {
        int[] rows = [0, Width - 1];
        for (var i = 0; i < rows.Length; i++)
        {
            for (var j = 0; j < _cargo.GetLength(1); j++)
            {
                var stack = _cargo[j, rows[i]];
                if (stack.Size != 0) continue;
                stack.Add(c);
                return;
            }
        }

    }

    private void PlaceCoolableContainer(Container c)
    {
        var row = 0;
        for (var i = 0; i < _cargo.GetLength(1); i++)
        {
            var stack = _cargo[i, row];
            stack.Add(c);
            if (stack.Weight < 150)
            {
                stack.Add(c);
                return;
            }
        }
    }

    private void PlaceCoolableValueableContainer(Container c)
    {
        var row = 0;
        for (var i = 0; i < _cargo.GetLength(1); i++)
        {
            var stack = _cargo[i, row];
            if (stack.Size != 0) continue;
            stack.Add(c);
            return;
        }
    }

    private void ReverseStacks()
    {
        for (int i = 0; i < _cargo.GetLength(0); i++)
        {
            for (int j = 0; j < _cargo.GetLength(1); j++)
            {
                _cargo[i, j].Reverse();
            }
        }
    }

    private void FillArray()
    {
        for (int i = 0; i < _cargo.GetLength(0); i++)
        {
            for (int j = 0; j < _cargo.GetLength(1); j++)
            {
                _cargo[i, j] = new Stack();
            }
        }
    }
}