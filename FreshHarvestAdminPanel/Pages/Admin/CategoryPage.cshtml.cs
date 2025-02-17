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

        private readonly HttpClient _httpClient;

        public CategoryPageModel(CategoryService categoryService, HttpClient httpClient)
        {
            _categoryService = categoryService;
            _httpClient = httpClient;
        }

        public List<CategoryModel> GetCategories { get; set; }

        public async Task OnGetAsync()
        {
            GetCategories = await _categoryService.GetCategoriesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7217/api/Category/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to Delete Category");
                return Page();
            }

            return RedirectToPage("/Admin/CategoryPage");
        }
    }
    

    
}
