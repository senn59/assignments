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
    private readonly BakeryService _bakeryService;
    public Index(BakeryService bakeryService)
    {
        _bakeryService = bakeryService;
        Sandwiches = _bakeryService.Bakery.GetAvailableSandwiches();
        BakeryName = _bakeryService.Bakery.Name;
    }
    
    public void OnGet()
    {
        
    }
}