namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        //Here we need to inject the HTTPClient
        public readonly HttpClient _http;
        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; } = "Loading Products...";

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl is null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            if (result is not null && result.Data is not null)
                Products = result.Data;

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestion/{searchText}");
            return result.Data;
        }

        public async Task SearchProducts(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}");
            if (result is not null && result.Data is not null)
                Products = result.Data;
            if(Products.Count == 0)
                Message = "No products found.";
            
                ProductsChanged?.Invoke();
        }
    }
}
