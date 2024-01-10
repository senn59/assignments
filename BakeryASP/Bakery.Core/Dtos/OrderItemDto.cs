namespace Bakery.Core.Dtos;

public class OrderItemDto
{
    public required string SandwichName { get; set; }
    public required double SandwichPrice { get; set; }
    public required int Amount { get; set; }
}