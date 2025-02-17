using FreshHarvestAdminPanel.Services;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class EditProductPageModel : PageModel
    {
        private readonly EditProductService _editProductService;

        private readonly CategoryService _categoryService;

        public EditProductPageModel(EditProductService editProductService, CategoryService categoryService)
        {
            _editProductService = editProductService;
            _categoryService = categoryService;
        }

        public List<CategoryModel> category { get; set; } = new();


        [BindProperty]

        public ProductModel Product { get; set; } = new ProductModel();


        public async Task<IActionResult> OnGetAsync(int id)
        {
            category = await _categoryService.GetCategoriesAsync();
            
            var product = await _editProductService.GetProductByIdAsync(id);

            if (product == null)
            {
                return BadRequest();
            }

            Product = product;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            category = await _categoryService.GetCategoriesAsync();

            if (!ModelState.IsValid)
            {

                return Page();

            }

            if (Product == null)
            {
                return Page();
            }


            var success = await _editProductService.EditProductAsync(Product);

            if (success)
            {
                return RedirectToPage("/Admin/ProductPage");
            }

            ModelState.AddModelError("", "Failed to Edit the product");
            return Page();
        }

    }
}
