namespace ContainerTransport.Core;

public class Ship
{
    private readonly Stack[,] _cargo;
    
    public int Length { get; private init; }
    public int Width { get; private init; }
    public int MaxWeight { get; set; }
    public float WeightDifference => CalculateWeightDifference(Cargo);
    public Stack[,] Cargo => (Stack[,])_cargo.Clone();
    private bool _startOnX;
    private int _loops = 0;

    public Ship(int width, int length)
    {
        Length = length;
        Width = width;
        MaxWeight = length * width * 150; //max stack size
        _cargo = new Stack[width, length];
        FillArray();
    }

    public void Load(List<Container> containers, bool startOnX)
    {
        _startOnX = startOnX;
        FillArray();
        containers = containers.OrderByDescending(c => c.Type).ThenByDescending(c => c.Load).ToList();
        containers.ForEach(PlaceContainer);
        Console.WriteLine($"startonX: {startOnX}  loops: {_loops}");
    }

    private void PlaceContainer(Container c)
    {
        var optimalPlace = FindOptimalPlace(c);
        _loops += optimalPlace.loopCount;
        _cargo[optimalPlace.res.X, optimalPlace.res.Y].Add(c);
    }
    
    private (int loopCount, PlaceResult res) FindOptimalPlace(Container container)
    {
        var availablePlaces = new List<PlaceResult>();
        var loopCount = 0;
        if (!_startOnX)
        {
            var xWhereAvailablePlace = new List<int>();
            for (var y = 0; y < Length; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (xWhereAvailablePlace.Contains(x)) continue;
                    loopCount++;
                    if (CanPlace(container, x, y))
                    {
                        xWhereAvailablePlace.Add(x);
                        var cargoCopy = CreateCopy();
                        cargoCopy[x, y].Add(container);
                        availablePlaces.Add(new PlaceResult(x, y, CalculateWeightDifference(cargoCopy)));
                    }
                }
            }
        }
        else
        {
            for (var x = 0; x < Width; x++)
            {
                bool foundPlace = false;
                for (var y = 0; y < Length; y++)
                {
                    if (foundPlace) continue;
                    loopCount++;
                    if (CanPlace(container, x, y))
                    {
                        foundPlace = true;
                        var cargoCopy = CreateCopy();
                        cargoCopy[x, y].Add(container);
                        availablePlaces.Add(new PlaceResult(x, y, CalculateWeightDifference(cargoCopy)));
                    }
                }
            }
        }

        if (!availablePlaces.Any())
        {
            Console.WriteLine(container.Type);
            throw new Exception("Could not place container");
        }
        return (loopCount, availablePlaces.OrderBy(r => r.WeightDifference).First());

    }

    private bool CanPlace(Container c, int x, int y)
    {
        var hasValuableContainer = _cargo[x, y].HasValuableContainer();
        var isLightEnough = _cargo[x, y].IsLightEnoughFor(c);
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

        for (var i = 0; i < cargo.GetLength(0); i++)
        {
            for (var j = 0; j < cargo.GetLength(1); j++)
            {
                if (i < Width / 2)
                {
                    weightLeft += cargo[i, j].Weight;
                }
                else
                {
                    weightRight += cargo[i, j].Weight;
                }
            }
        }
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