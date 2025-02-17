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
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if(!response.IsSuccessStatusCode)
            {
                return new List<CategoryModel>();
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonString);
            var categoriesJson = jsonObject["$values"]?.ToString();


            return categoriesJson != null
                ? JsonConvert.DeserializeObject<List<CategoryModel>>(categoriesJson)
                : new List<CategoryModel>();

        }



    }

}