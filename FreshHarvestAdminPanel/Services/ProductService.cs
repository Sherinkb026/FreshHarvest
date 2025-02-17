using FreshHarvestAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace FreshHarvestAdminPanel.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7217/api/Product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<List<ProductModel>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<ProductModel>();
            }

            var jsonString = await response.Content.ReadAsStringAsync(); 
            return JsonConvert.DeserializeObject<List<ProductModel>>(jsonString) ?? new List<ProductModel>();
                                                                                                             
        }



    }
    
}
