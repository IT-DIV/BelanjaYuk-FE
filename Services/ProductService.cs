using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Response;

namespace BelanjaYuk.Client.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductResponse>> GetProductsAsync(string? searchQuery = null)
    {
        try
        {
            var url = "/api/v1/products";
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                url += $"?searchQuery={Uri.EscapeDataString(searchQuery)}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
                return result ?? new List<ProductResponse>();
            }

            return new List<ProductResponse>();
        }
        catch
        {
            return new List<ProductResponse>();
        }
    }

    public async Task<List<ProductResponse>> GetProductsByCategoryAsync(string categoryId, string? searchQuery = null)
    {
        try
        {
            var url = $"/api/v1/products/category/{categoryId}";
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                url += $"?searchQuery={Uri.EscapeDataString(searchQuery)}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
                return result ?? new List<ProductResponse>();
            }

            return new List<ProductResponse>();
        }
        catch
        {
            return new List<ProductResponse>();
        }
    }
}
