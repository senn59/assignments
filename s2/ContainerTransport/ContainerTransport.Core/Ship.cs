namespace ContainerTransport.Core;

public class Ship
{
    private readonly Stack?[,] _cargo;
    
    public int Length { get; private init; }
    public int Width { get; private init; }
    public Stack?[,] Cargo => (Stack?[,])_cargo.Clone();

    public Ship(int width, int length)
    {
        Length = length;
        Width = width;
        _cargo = new Stack?[width, length];
        Console.WriteLine(_cargo.GetLength(0));
        Console.WriteLine(_cargo.GetLength(1));
    }
}