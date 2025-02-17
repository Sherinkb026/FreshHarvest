using System.Text.Json;
using FreshHarvestAPI.Models;
using System.Text;

namespace FreshHarvestAdminPanel.Services
{
    public class AddCategoryService
    {

        private readonly HttpClient _httpClient;

        private readonly string _apiBaseUrl = "https://localhost:7217/api/Category";//stores the api url where the new categories will be send 

        public AddCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //method to add a category
        public async Task<bool> AddCategoryAsync(CategoryModel category)
        {
            var json = JsonSerializer.Serialize(category);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
                                                                                                                                                             
            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
                                                                             
            return response.IsSuccessStatusCode;

        }
    }
}
