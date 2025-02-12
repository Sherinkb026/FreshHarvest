using System.Text.Json;
using FreshHarvestAPI.Models;
using System.Text;

namespace FreshHarvestAdminPanel.Services
{
    public class AddCategoryService
    {

        public readonly HttpClient _httpClient;

        public readonly string _apiBaseUrl = "https://localhost:7217/api/Category";//stores the api url where the new categories will be send 

        public AddCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //method to add a category
        public async Task<bool> AddCategoryAsync(CategoryModel category)
        {
            var json = JsonSerializer.Serialize(category);//converts category object into json format

            var content = new StringContent(json, Encoding.UTF8, "application/json");//preparing http content
                                                                                     //json: json data
                                                                                     //Encoding.UTF8 : specifies UTF8 encoding (to handle special characters correctly
                                                                                     //application/json : tells the api that we are sending json data
            //send data to the api
            var response = await _httpClient.PostAsync(_apiBaseUrl, content);//sends a post request to apiBaseUrl
                                                                             //the content(json data) is included in the request 
            return response.IsSuccessStatusCode;

        }
    }
}
