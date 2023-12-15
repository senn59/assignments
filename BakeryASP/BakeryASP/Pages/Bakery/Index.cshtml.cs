using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Index : PageModel
{
    [BindProperty]
    public string Title { get; set; } = string.Empty;
    
    public void OnGet()
    {
        Console.WriteLine("on get");
    }

    public IActionResult OnPost(string title)
    {
        Console.WriteLine(title);
        Console.WriteLine(Title);
        return Page();
    }
}