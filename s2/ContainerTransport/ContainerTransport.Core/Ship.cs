namespace ContainerTransport.Core;

public class Ship
{
    private readonly Stack[,] _cargo;
    
    public int Length { get; private init; }
    public int Width { get; private init; }
    public int MaxWeight { get; set; }
    public float WeightDifference => CalculateWeightDifference(Cargo);
    public Stack[,] Cargo => (Stack[,])_cargo.Clone();

    public Ship(int width, int length, List<Container> containers)
    {
        Length = length;
        Width = width;
        MaxWeight = length * width * 150; //max stack size
        _cargo = new Stack[width, length];
        FillArray();
        containers = containers.OrderByDescending(c => c.Type).ToList();
        containers.ForEach(PlaceContainer);
        ReverseStacks();
    }

    private void PlaceContainer(Container c)
    {
        var leftStart = TryPlace(c);
        var rightStart = TryPlace(c, true);
        int x;
        int y;
        // Console.WriteLine($"left: {leftStart.WeightDifference} right: {rightStart.WeightDifference}");
        if (leftStart.WeightDifference < rightStart.WeightDifference)
        {
            x = leftStart.Coords.x;
            y = leftStart.Coords.y;
        }
        else
        {
            x = rightStart.Coords.x;
            y = rightStart.Coords.y;
        }
        _cargo[x, y].Add(c);
    }

    private ((int x, int y) Coords, float WeightDifference) TryPlace(Container c,bool reverse = false)
    {
        var initVal = reverse ? Width - 1 : 0;
        var endVal = reverse ? -1 : Width;
        var step = reverse ? -1 : +1;

        for (var x = initVal; x != endVal; x += step)
        {
            for (var y = 0; y < Length; y++)
            {
                if (CanPlace(c, x, y))
                {
                    var cargoCopy = CreateCopy();
                    cargoCopy[x, y].Add(c);
                    return ((x, y), CalculateWeightDifference(cargoCopy));
                }
            }
        }
        throw new Exception("Could not place container");
    }

    private bool CanPlace(Container c, int x, int y)
    {
        var hasValuableContainer = _cargo[x, y].Containers.Any(c => 
            c.Type is ContainerType.CoolableValuable or ContainerType.Valuable);
        var isLightEnough = _cargo[x, y].Weight < 150;
        switch (c.Type)
        {
            case ContainerType.Normal:
                return isLightEnough;
            case ContainerType.Valuable:
                return (y == 0 || y == Length - 1) && !hasValuableContainer;
            case ContainerType.Coolable:
                return y == 0 && isLightEnough;
            case ContainerType.CoolableValuable:
                return y == 0 && !hasValuableContainer;
        }
        
        throw new Exception("Not a valid container");
    }

    private void ReverseStacks()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                _cargo[i, j].Reverse();
            }
        }
    }

    private void FillArray()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                _cargo[i, j] = new Stack();
            }
        }
    }

    private float CalculateWeightDifference(Stack[,] cargo)
    {
        int? middleRow = null;
        if (Width % 2 == 1)
        {
            middleRow = Width / 2 + 1;
        }

        float weightLeft = 0;
        float weightRight = 0;
        float weightMiddle = 0;

        for (var i = 0; i < cargo.GetLength(0); i++)
        {
            for (var j = 0; j < cargo.GetLength(1); j++)
            {
                if (i == middleRow - 1)
                {
                    weightMiddle += cargo[i, j].Weight;
                }
                else if (i < middleRow - 1)
                {
                    weightLeft += cargo[i, j].Weight;
                }
                else
                {
                    weightRight += cargo[i, j].Weight;
                }
            }
        }
        weightLeft += weightMiddle / 2;
        weightRight += weightMiddle / 2;
        var loadedWeight = weightLeft + weightRight;
        var left = weightLeft / loadedWeight * 100;
        var right = weightRight / loadedWeight * 100;
        return Math.Abs(left - right);
    }

    private Stack[,] CreateCopy()
    {
        var copy = (Stack[,])_cargo.Clone();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Length; y++)
            {
                var stack = copy[x, y];
                copy[x, y] = new Stack();
                foreach (var c in stack.Containers)
                {
                    copy[x, y].Add(c);
                }
            }
        }

        return copy;
    }
}