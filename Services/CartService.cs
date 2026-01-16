using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Request;

namespace BelanjaYuk.Client.Services;

public class CartService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public CartService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<(bool Success, string Message)> AddToCartAsync(string userId, string productId, int quantity = 1)
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var request = new
            {
                userId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            var response = await _httpClient.PostAsJsonAsync("/api/v1/cart/add", request);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Barang ditambahkan ke keranjang.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return (false, "Anda harus login terlebih dahulu.");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}");
        }
    }
}
