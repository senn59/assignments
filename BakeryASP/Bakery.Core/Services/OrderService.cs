using System.Text.Json;
using Bakery.Core.Dtos;

namespace Bakery.Core.Services;

public class OrderService
{

    public void Insert(OrderDto order)
    {
        string filePath = "order-data.json";
        string orderContents = File.Exists(filePath) ? File.ReadAllText(filePath) : "[]";
        List<OrderDto>? orders = JsonSerializer.Deserialize<List<OrderDto>>(orderContents) ?? new List<OrderDto>();

        orders.Add(order);

        var orderContentsNew = JsonSerializer.Serialize(orders);
        File.WriteAllText(filePath, orderContentsNew);
    }

    public IEnumerable<OrderDto> GetAll()
    {
        string filePath = "order-data.json";
        if (!File.Exists(filePath)) return [];
        string orderContents = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<OrderDto>>(orderContents) ?? new List<OrderDto>();
        
        // converteer de DTOs naar echte order en order item classes
        // TotalPrice
    }

    public decimal GetRevenue()
    {
        return GetAll().Sum(o => o.Sandwiches.Sum(s => s.TotalPrice));
    }
}