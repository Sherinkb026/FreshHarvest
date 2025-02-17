using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;

namespace FreshHarvestAdminPanel.Services
{
    public class EditProductService
    {

        private readonly HttpClient _httpClient;

        private readonly string _apiBaseUrl = "https://localhost:7217/api/Product";

        public EditProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            string url = $"{_apiBaseUrl}/{id}";

            var response = await _httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();

            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            var product = JsonConvert.DeserializeObject<ProductModel>(responseBody);

            return product;

        }


        public async Task<bool> EditProductAsync(ProductModel product)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(product);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{product.Id}", content);

            return response.IsSuccessStatusCode;
        }
    }
}
