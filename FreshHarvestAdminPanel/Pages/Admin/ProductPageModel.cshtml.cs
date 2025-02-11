using FreshHarvestAdminPanel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreshHarvestAPI.Models;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class ProductPageModelModel : PageModel
    {
        private readonly ProductService _productService;

        public ProductPageModelModel(ProductService productService)
        {
            _productService = productService;
        }

        public List<ProductModel> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _productService.GetProductsAsync();
           
        }
    }
}
