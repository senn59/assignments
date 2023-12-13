using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages.Bakery;

public class Index : PageModel
{
    public string Title { get; set; } = string.Empty;
    public void OnGet()
    {
        Title = "Bakery";
    }

    public void OnPost()
    {
        
    }
}