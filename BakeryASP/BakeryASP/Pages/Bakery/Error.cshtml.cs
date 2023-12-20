using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Error : PageModel
{
    [BindProperty] public string Heading { get; set; }
    [BindProperty] public string Description { get; set; }
    public void OnGet(string heading, string description)
    {
        Console.WriteLine(heading);
        Console.WriteLine(description);
    }
}