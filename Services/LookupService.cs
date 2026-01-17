using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Response;

namespace BelanjaYuk.Client.Services;

public class LookupService
{
    private readonly HttpClient _httpClient;

    public LookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GenderResponse>> GetGendersAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<GenderResponse>>("/api/v1/lookup/genders");
            return response ?? new List<GenderResponse>();
        }
        catch
        {
            return new List<GenderResponse>();
        }
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

    public async Task<List<PaymentOptionResponse>> GetPaymentOptionsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/v1/lookup/payment-options");

            if (response.IsSuccessStatusCode)
            {
                var paymentOptions = await response.Content.ReadFromJsonAsync<List<PaymentOptionResponse>>();
                return paymentOptions ?? new List<PaymentOptionResponse>();
            }

            return new List<PaymentOptionResponse>();
        }
        catch
        {
            return new List<PaymentOptionResponse>();
        }
    }
}
