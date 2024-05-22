namespace Bakery.Core.Dtos;

public class OrderDto
{
    public IEnumerable<OrderItemDto> Sandwiches { get; set; }

    public required DateTime DateCreated { get; set; }
}