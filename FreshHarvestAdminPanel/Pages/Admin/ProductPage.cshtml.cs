using FreshHarvestAdminPanel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreshHarvestAPI.Models;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class ProductPageModel : PageModel
    {
        private readonly ProductService _productService;

        private readonly HttpClient _httpClient;

        public ProductPageModel(ProductService productService, HttpClient httpClient)
        {
            _productService = productService;
            _httpClient = httpClient;
        }

        public List<ProductModel> Products { get; set; }

        public async Task OnGetAsync()
                                      
        {
            Products = await _productService.GetProductsAsync();
           
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7217/api/Product/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to delete the product");
                return Page();
            }

            return RedirectToPage("/Admin/ProductPage");
        }
    }
}
