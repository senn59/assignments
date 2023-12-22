using Bakery.Core;
using BakeryASP.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Index : PageModel
{
    public string Title { get; set; } = "Bakery";
    public string BakeryName { get; private set; }
    public IReadOnlyList<Sandwich> Sandwiches { get; private set; }
    public List<String> Breads { get; set; } = Enum.GetNames<BreadType>().ToList();
    private readonly BakeryService _bakeryService;
    
    public Index(BakeryService bakeryService)
    {
        this._bakeryService = bakeryService;
        this.Sandwiches = _bakeryService.Bakery.GetAvailableSandwiches();
        this.BakeryName = _bakeryService.Bakery.Name;
    }
    
    public void OnGet()
    {
        
    }
}