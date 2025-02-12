using FreshHarvestAPI.Models;
using Newtonsoft.Json;//used to convert json data to c# objects and vice versa
using Newtonsoft.Json.Linq;





namespace FreshHarvestAdminPanel.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;//a private variable to store the HttpObject, used to make http requests.
        private readonly string _apiBaseUrl = "https://localhost:7217/api/Product";//stores the API url where product data can be fetched.

        public ProductService(HttpClient httpClient)//allows the service to use HttpClient for making API calls
        {
            _httpClient = httpClient;
        }

        
        public async Task<List<ProductModel>> GetProductsAsync()//An asynchronous method that fetches the list of products from the API. and returns a List<ProductModel>
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);//calls the api using HttpClient and sends a get request to _apiBaseUrl

            if (!response.IsSuccessStatusCode)//checks if the api request was unsuccessfull 
            {
                return new List<ProductModel>();//if it failed, it returns an empty list. 
            }

            var jsonString = await response.Content.ReadAsStringAsync(); //reads the json response which is in json format as string 
            return JsonConvert.DeserializeObject<List<ProductModel>>(jsonString) ?? new List<ProductModel>();//converts the json string into a List<productModel> using json.DesrializeObject
                                                                                                             //if the conversion fails , it returns an empty list instead of null 
        }



    }
    
}
