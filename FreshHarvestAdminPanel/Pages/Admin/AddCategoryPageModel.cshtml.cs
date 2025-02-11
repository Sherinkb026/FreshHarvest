using FreshHarvestAdminPanel.Services;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreshHarvestAdminPanel.Services;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class AddCategoryPageModelModel : PageModel
    {

        private readonly AddCategoryService _addcategoryService;

        public AddCategoryPageModelModel(AddCategoryService addcategoryService)
        {
            _addcategoryService = addcategoryService;
        }

        [BindProperty]

        public CategoryModel NewCategory { get; set; } = new CategoryModel();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = await _addcategoryService.AddCategoryAsync(NewCategory);
            if (success) {
                return RedirectToPage("/Admin/CategoryPageModel");
            }

            ModelState.AddModelError("", "Failed to Add Category");
            return Page();
        }
        
    }
}
