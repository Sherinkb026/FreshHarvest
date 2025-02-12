using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using FreshHarvestAPI.Models;
using FreshHarvestAdminPanel.Services;
using FreshHarvestAPI.Models;
using FreshHarvestAdminPanel.Pages.Admin;
using Newtonsoft.Json.Linq;




namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class CategoryPageModel : PageModel
    {
        private readonly CategoryService _categoryService;

        public CategoryPageModel(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<CategoryModel> GetCategories { get; set; }

        public async Task OnGetAsync()
        {
            GetCategories = await _categoryService.GetCategoriesAsync();
        }
    }
    

    
}
