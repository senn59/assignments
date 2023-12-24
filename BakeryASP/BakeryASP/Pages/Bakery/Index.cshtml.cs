using Bakery.Core;
using BakeryASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Index : PageModel
{
    public string Title { get; set; } = "Bakery";
    public string BakeryName { get; private set; }
    public IReadOnlyList<Sandwich> Sandwiches { get; private set; }
    public List<String> Breads { get; set; } = Enum.GetNames<BreadType>().ToList();
    [BindProperty] public Dictionary<string, int> Cart { get; set; } = new Dictionary<string, int>();
    private readonly BakeryService _bakeryService;
    
    public Index(BakeryService bakeryService)
    {
        this._bakeryService = bakeryService;
        this.Sandwiches = _bakeryService.Bakery.GetAvailableSandwiches();
        foreach (var s in this.Sandwiches)
        {
            Cart.Add(s.Name, 0);
        }
        this.BakeryName = _bakeryService.Bakery.Name;
    }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        foreach (var item in Cart) 
        {
            Console.WriteLine(item.Key);
            Console.WriteLine(item.Value);
        }

        return RedirectToPage("/Bakery/Index");
    }
}