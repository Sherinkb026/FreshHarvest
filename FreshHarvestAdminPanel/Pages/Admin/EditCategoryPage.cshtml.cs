using FreshHarvestAdminPanel.Services;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class EditCategoryPageModel : PageModel
    {

        private readonly EditCategoryService _editCategoryService;

        public EditCategoryPageModel(EditCategoryService editCategoryService)
        {
            _editCategoryService = editCategoryService;
        }

        [BindProperty]

        public CategoryModel Category { get; set; } = new CategoryModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await _editCategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            Category = category;

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _editCategoryService.EditCategoryAsync(Category);

            if (success)
            {
                return Redirect("/Admin/CategoryPage");
            }

            ModelState.AddModelError("", "Failed to update category");

            return Page();
            
        }
    }
}
