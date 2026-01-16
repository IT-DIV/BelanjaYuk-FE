using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Response;

namespace BelanjaYuk.Client.Services;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoryResponse>> GetCategoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/v1/lookup/categories");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<CategoryResponse>>();
                return result ?? new List<CategoryResponse>();
            }

            return new List<CategoryResponse>();
        }
        catch
        {
            return new List<CategoryResponse>();
        }
    }
}
