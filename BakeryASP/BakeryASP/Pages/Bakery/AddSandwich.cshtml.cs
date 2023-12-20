using Bakery.Core;
using BakeryASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BakeryASP.Pages.Bakery;

public class AddSandwich : PageModel
{
    [BindProperty] public required string Name { get; set; } = string.Empty;
    [BindProperty] public string Bread { get; set; } = string.Empty;
    [BindProperty] public List<string> IngredientList { get; set; }
    [BindProperty] public string Price { get; set; }

    public IReadOnlyList<Ingredient> Ingredients;

    public List<SelectListItem> AvailableBreads { get; private set; } = Enum.GetNames<BreadType>()
        .Select(bt => new SelectListItem(bt, bt))
        .ToList();

    private readonly BakeryService _bakeryService;
    private readonly ILogger<AddSandwich> _logger;

    public AddSandwich(BakeryService bakeryService, ILogger<AddSandwich> logger)
    {
        this._bakeryService = bakeryService;
        _logger = logger;
        this.Ingredients = _bakeryService.Bakery.Ingredients;
    }

    public IActionResult OnGet()
    {
        _logger.LogInformation("Someone wants to add a sandwich");
        return Page();
    }

    public IActionResult OnPost()
    {
        _logger.LogInformation("Going to insert new sandwich");
        if (!Enum.TryParse(Bread, out BreadType breadType))
        {
            _logger.LogDebug("Bread value {bread}", Bread);
            ModelState.AddModelError(nameof(Bread), "Unknown bread type");
        }

        if (!Double.TryParse(Price, out var parsedPrice))
        {
            ModelState.AddModelError(nameof(Price), "Invalid price");
        }

        var newSandwich = new Sandwich(Name, breadType, parsedPrice);
        List<string> ingredientErrors = new List<string>();
        foreach (var i in IngredientList)
        {
            var ingredient = _bakeryService.Bakery.Ingredients.FirstOrDefault(x => x.Name == i);
            if (ingredient == null)
            {
                continue;
            }

            var result = newSandwich.AddIngredient(ingredient);
            if (result != null)
            {
                ingredientErrors.Add(result);
            }
        }

        if (ingredientErrors.Count > 0)
        {
            ModelState.AddModelError(nameof(Ingredients), string.Join(", ", ingredientErrors));
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Sandwich was not added correctly, because user is stupid");
            return Page();
        }

        _bakeryService.Bakery.AddSandwich(newSandwich);
        _logger.LogInformation("Successfully added sanwich {name}", newSandwich.Name);
        return RedirectToPage("Index");
    }
}