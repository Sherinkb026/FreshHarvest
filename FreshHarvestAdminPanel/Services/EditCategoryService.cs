using FreshHarvestAPI.Models;
using System.Text;
using Newtonsoft.Json;


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


        public async Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            string url = $"{_apiBaseUrl}/{id}";
          

            var response = await _httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var category = JsonConvert.DeserializeObject<CategoryModel>(responseBody);

            return category;
        }

        public async Task<bool> EditCategoryAsync(CategoryModel category)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(category);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{category.Id}", content);

            return response.IsSuccessStatusCode;

        }

       

    }
}
