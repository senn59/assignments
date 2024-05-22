using Bakery.Core;
using Bakery.Core.Dtos;
using Bakery.Core.Services;
using BakeryASP.Models;
using BakeryASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Index : PageModel
{
    public string Title { get; set; } = "Bakery";
    public string BakeryName { get; private set; }
    public IReadOnlyList<Sandwich> Sandwiches { get; private set; }
    public List<string> Breads { get; set; } = Enum.GetNames<BreadType>().ToList();

    // [BindProperty] public Dictionary<string, int> Cart { get; set; } = new Dictionary<string, int>();
    [BindProperty] public List<OrderItemView> Cart { get; set; }
    public decimal Revenue { get; set; } = 0;
    public decimal RevenueWithVat => Math.Round(((Revenue) * (100 + 21) / 100), 2);

    private readonly BakeryService _bakeryService;
    private readonly OrderService _orderService;
    private readonly TimeProvider _timeProvider;

    public Index(BakeryService bakeryService, OrderService orderService, TimeProvider timeProvider)
    {
        this._bakeryService = bakeryService;
        _orderService = orderService;
        _timeProvider = timeProvider;
        this.Sandwiches = _bakeryService.Bakery.GetAvailableSandwiches();
        Cart = new List<OrderItemView>(new OrderItemView[Sandwiches.Count]);
        this.BakeryName = _bakeryService.Bakery.Name;
    }

    public void OnGet()
    {
        Revenue = _orderService.GetRevenue();
    }

    public IActionResult OnPost()
    {
        Console.WriteLine(Cart.Count);
        var orders = new List<OrderItemDto>();
        foreach (var item in Cart)
        {
            var sandwich = Sandwiches.FirstOrDefault(s => s.Name == item.SandwichName);
            if (sandwich == null)
            {
                ModelState.AddModelError(nameof(Cart), "Invalid sandwich in cart");
                return Page();
            }

            if (item.Amount <= 0)
            {
               continue; 
            }
            
            orders.Add(new OrderItemDto()
            {
                SandwichName = item.SandwichName,
                Amount = item.Amount,
                SandwichPrice = sandwich.GetPrice()
            });

        }
        
        if (orders.Count == 0)
        {
            ModelState.AddModelError(nameof(Cart), "No items in cart.");
            return Page();
        }

        _orderService.Insert(new OrderDto
        {
            Sandwiches = orders,
            DateCreated = _timeProvider.GetUtcNow().DateTime
        });

        return RedirectToPage("/Bakery/Index");
    }
}