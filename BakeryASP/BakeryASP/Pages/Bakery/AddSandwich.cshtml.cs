using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class AddSandwich : PageModel
{
    [BindProperty] public string Name { get; set; }
    [BindProperty] public string Bread { get; set; }
    [BindProperty] public string Ingredient { get; set; }
    public void OnGet()
    {
        
    }
}