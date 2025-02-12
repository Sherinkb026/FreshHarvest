using FreshHarvestAdminPanel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreshHarvestAPI.Models;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class ProductPageModel : PageModel
    {
        private readonly ProductService _productService;

        public ProductPageModel(ProductService productService)
        {
            _productService = productService;
        }

        public List<ProductModel> Products { get; set; }//declares a public list to store products
                                                        //this stores data that will be displayed in the razorpage

        public async Task OnGetAsync()//handles the get request
                                      //this page automatically calls when the page loads
        {
            Products = await _productService.GetProductsAsync();//calls the api to fetch data and stores it in products 
           
        }
    }
}
