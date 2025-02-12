using System.Text.Json;
using FreshHarvestAdminPanel.Pages.Admin;
using FreshHarvestAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FreshHarvestAdminPanel.Services
{
    public class CategoryService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7217/api/Category";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<CategoryModel>?> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);//sends a get request to the api url
            if(!response.IsSuccessStatusCode)//checks if the api returned a succesful response
            {
                return new List<CategoryModel>();//if api request fails, return an empty list instead of breaking the program
            }

            var jsonString = await response.Content.ReadAsStringAsync();//reads the api response as string 
            var jsonObject = JObject.Parse(jsonString);//converts the json string into json object which can be queried like a dictionary. 
            var categoriesJson = jsonObject["$values"]?.ToString();//gets the "$Values" array which contains the actual category list


            return categoriesJson != null//catgeories is not null 
                ? JsonConvert.DeserializeObject<List<CategoryModel>>(categoriesJson)//it deserializes it into List<categorymodel>
                : new List<CategoryModel>();//else return an empty list

        }



    }

}