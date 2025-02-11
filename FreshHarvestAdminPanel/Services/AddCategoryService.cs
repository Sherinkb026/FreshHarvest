using System.Text.Json;
using FreshHarvestAPI.Models;
using System.Text;

namespace FreshHarvestAdminPanel.Services
{
    public class AddCategoryService
    {

        public readonly HttpClient _httpClient;

        public readonly string _apiBaseUrl = "https://localhost:7217/api/Category";

        public AddCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddCategoryAsync(CategoryModel category)
        {
            var json = JsonSerializer.Serialize(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            return response.IsSuccessStatusCode;

        }
    }
}
