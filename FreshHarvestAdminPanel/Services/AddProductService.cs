using System.Text.Json;
using FreshHarvestAPI.Models;
using System.Text;

namespace FreshHarvestAdminPanel.Services
{
    public class AddProductService
    {

        public readonly HttpClient _httpClient;

        public readonly string _apiBaseUrl = "https://localhost:7217/api/Product";


        public AddProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<bool> AddProductAsync(ProductModel product)
        {
            var json = JsonSerializer.Serialize(product);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);

            return response.IsSuccessStatusCode;

        }


    }
}
