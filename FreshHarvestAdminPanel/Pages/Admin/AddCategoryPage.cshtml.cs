using FreshHarvestAdminPanel.Services;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FreshHarvestAdminPanel.Services;

namespace FreshHarvestAdminPanel.Pages.Admin
{
    public class AddCategoryPageModel : PageModel
    {

        private readonly AddCategoryService _addcategoryService;

        public AddCategoryPageModel(AddCategoryService addcategoryService)//allows us to call the api when a new category is added
        {
            _addcategoryService = addcategoryService;
        }


        //this creates a property to store the category details entered by the user
        [BindProperty]//automatically binds form inputs to NewCategory

        public CategoryModel NewCategory { get; set; } = new CategoryModel();//creates an empty category object where the form data will be stored. 



        //handling form submission
        //this runs when the user submits the form 
        public async Task<IActionResult> OnPostAsync()//onpostasync is triggered when the user click add category button
        {
            if (!ModelState.IsValid)//checks the form validation 
                return Page();//if the input is invalid, it reloads the page.

            var success = await _addcategoryService.AddCategoryAsync(NewCategory);//calls the addcategoryservice to add the new category to the api

            if (success) {
                return RedirectToPage("/Admin/CategoryPage");
            }

            ModelState.AddModelError("", "Failed to Add Category");
            return Page();
        }
        
    }
}
