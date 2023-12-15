using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BakeryASP.Pages;

public class Index : PageModel
{
    public IActionResult OnGet()
    {
        return new RedirectToPageResult("/Bakery/Index");
    }
}