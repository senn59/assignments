namespace ContainerTransport.Core;

public class ContainerPlacementResult(int x, int y, float weightDifference)
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;
    public float WeightDifference { get; init; } = weightDifference;
}