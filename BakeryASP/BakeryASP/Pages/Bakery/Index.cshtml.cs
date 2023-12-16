using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Index : PageModel
{
    [BindProperty]
    public string Title { get; set; } = string.Empty;
    
    public void OnGet(string title)
    {
        Title = title;
    }

    public IActionResult OnPost(string? title)
    {
        return RedirectToPage("/Bakery/Index", new {title = Title });
    }
}