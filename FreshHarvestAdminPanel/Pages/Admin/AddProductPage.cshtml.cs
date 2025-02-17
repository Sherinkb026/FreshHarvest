using FreshHarvestAPI.Data;
using FreshHarvestAdminPanel.Services;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class AddProductPageModel : PageModel
    {
        private readonly AddProductService _addProductService;

        private readonly CategoryService _categoryService;

        public AddProductPageModel(AddProductService addProductService, CategoryService categoryService)
        {
            _addProductService = addProductService;
            _categoryService = categoryService;
        }



        public List<CategoryModel> category { get; set; } = new();


        [BindProperty]
        public ProductModel Product { get; set; } = new ProductModel();


        public async Task<IActionResult> OnGetAsync()
        {
            category = await _categoryService.GetCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            category = await _categoryService.GetCategoriesAsync();

            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            var success = await _addProductService.AddProductAsync(Product);

            if (success)
            {
                return RedirectToPage("/Admin/ProductPage");
                
            }
            
            ModelState.AddModelError("", "Failed to add the product");
            return Page();



        }

    }
}
