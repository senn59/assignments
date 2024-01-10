using Bakery.Core;
using Bakery.Core.Dtos;
using Bakery.Core.Services;
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

    [BindProperty] public Dictionary<string, int> Cart { get; set; } = new Dictionary<string, int>();
    public double revenue { get; set; } = 0;

    private readonly BakeryService _bakeryService;
    private readonly OrderService _orderService;
    private readonly TimeProvider _timeProvider;

    public Index(BakeryService bakeryService, OrderService orderService, TimeProvider timeProvider)
    {
        this._bakeryService = bakeryService;
        _orderService = orderService;
        _timeProvider = timeProvider;
        this.Sandwiches = _bakeryService.Bakery.GetAvailableSandwiches();
        foreach (var s in this.Sandwiches)
        {
            Cart.Add(s.Name, 0);
        }

        this.BakeryName = _bakeryService.Bakery.Name;
    }

    public void OnGet()
    {
        var orders = _orderService.GetAll();
        foreach (var order in orders)
        {
            foreach (var sandwich in order.Sandwiches)
            {
                this.revenue += sandwich.SandwichPrice * sandwich.Amount;
            }
        }
    }

    public IActionResult OnPost()
    {
        var orders = new List<OrderItemDto>();
        foreach (var item in Cart.Where(kvp => !kvp.Key.Contains("Request")))
        {
            var sandwich = this.Sandwiches.FirstOrDefault(s => s.Name == item.Key);
            if (sandwich == null)
            {
                ModelState.AddModelError(nameof(Cart), "Invalid sandwich in cart.");
                return Page();
            }

            if (item.Value == 0)
            {
                continue;
            }

            orders.Add(new OrderItemDto
            {
                SandwichName = sandwich.Name,
                SandwichPrice = sandwich.GetPrice(),
                Amount = item.Value
            });
        }

        // List<OrderItemDto> orders = Cart
        //     .Where(kvp => !kvp.Key.Contains("Request"))
        //     .Select(kvp =>
        //     {
        //         var sandwich = this.Sandwiches.FirstOrDefault(s => s.Name == kvp.Key) ??
        //                        throw new InvalidOperationException("This should not happen. Hackerz?");
        //
        //         return new OrderItemDto
        //         {
        //             SandwichName = sandwich.Name,
        //             SandwichPrice = sandwich.GetPrice(),
        //             Amount = kvp.Value
        //         };
        //     }).ToList();

        // _bakeryService.Bakery.Order(order);
        _orderService.Insert(new OrderDto
        {
            Sandwiches = orders,
            DateCreated = _timeProvider.GetUtcNow().DateTime
        });

        return RedirectToPage("/Bakery/Index");
    }
}