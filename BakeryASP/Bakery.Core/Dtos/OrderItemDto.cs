namespace Bakery.Core.Dtos;

public class OrderItemDto
{
    public required string SandwichName { get; set; }
    public required decimal SandwichPrice { get; set; }
    public required int Amount { get; set; }

    public decimal TotalPrice => SandwichPrice * Amount;
}