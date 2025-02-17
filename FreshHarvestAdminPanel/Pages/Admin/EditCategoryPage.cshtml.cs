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

        public async Task<IActionResult> OnGetAsync(int Id)
        {

            var category = await _editCategoryService.GetCategoryByIdAsync(Id);

            if (category == null)
            {
                return BadRequest();
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

            if (Category == null)
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
