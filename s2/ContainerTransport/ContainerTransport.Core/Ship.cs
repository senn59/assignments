namespace ContainerTransport.Core;

public class Ship
{
    private readonly Stack[,] _cargo;
    
    public int Length { get; private init; }
    public int Width { get; private init; }
    public int MaxWeight { get; set; }
    public float WeightDifference => CalculateWeightDifference(Cargo);
    public Stack[,] Cargo => (Stack[,])_cargo.Clone();

    public Ship(int width, int length)
    {
        Length = length;
        Width = width;
        MaxWeight = length * width * 150; //max stack size
        _cargo = new Stack[width, length];
        FillArray();
    }

    public void Load(List<Container> containers)
    {
        FillArray();
        containers = containers.OrderByDescending(c => c.Type).ToList();
        containers.ForEach(PlaceContainer);
    }

    private void PlaceContainer(Container c)
    {
        var optimalPlace = FindOptimalPlace(c);
        _cargo[optimalPlace.X, optimalPlace.Y].Add(c);
    }
    
    private PlaceResult FindOptimalPlace(Container container)
    {
        var availablePlaces = new List<PlaceResult>();
        for (var x = 0; x < Width; x++)
        {
            var foundPlace = false;
            for (var y = 0; y < Length; y++)
            {
                if (foundPlace) continue;
                if (CanPlace(container, x, y))
                {
                    foundPlace = true;
                    var cargoCopy = CreateCopy();
                    cargoCopy[x, y].Add(container);
                    availablePlaces.Add(new PlaceResult(x, y, CalculateWeightDifference(cargoCopy)));
                }
            }
        }

        if (!availablePlaces.Any())
        {
            Console.WriteLine(container.Type);
            throw new Exception("Could not place container");
        }
        return availablePlaces.OrderBy(r => r.WeightDifference).First();

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