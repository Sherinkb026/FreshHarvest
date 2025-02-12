using System.Text.Json;
using FreshHarvestAPI.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text;

namespace FreshHarvestAdminPanel.Services
{
    public class EditCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7217/api/Category";

        public EditCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EditCategoryAsync(CategoryModel category)
        {
            var json = JsonSerializer.Serialize(category);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{category.Id}", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<CategoryModel?> GetCategoryByIdAsync(int Id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("API Response:" + json);

            using var jsonDocument = JsonDocument.Parse(json);

            if (jsonDocument.RootElement.TryGetProperty("$values", out var valuesElement) && valuesElement.GetArrayLength()>0)
            {
                var categoryJson = valuesElement[0].GetRawText();
                return JsonSerializer.Deserialize<CategoryModel>(categoryJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            } 
            
            return null;

        }
    }
}
