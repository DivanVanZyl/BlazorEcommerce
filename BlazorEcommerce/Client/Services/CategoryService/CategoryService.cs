﻿namespace BlazorEcommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }
        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
            if(response is not null && response.Data is not null)
                Categories = response.Data;
        }
    }
}
