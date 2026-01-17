using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Request;
using BelanjaYuk.Client.Models.Response;

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

    public async Task<List<CartItemResponse>> GetCartAsync(string userId)
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync($"/api/v1/cart/cart/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemResponse>>();
                return cartItems ?? new List<CartItemResponse>();
            }

            return new List<CartItemResponse>();
        }
        catch
        {
            return new List<CartItemResponse>();
        }
    }

    public async Task<(bool Success, string Message)> UpdateCartQuantityAsync(string userId, string cartId, int newQuantity)
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
                IdBuyerCart = cartId,
                NewQty = newQuantity
            };

            var response = await _httpClient.PostAsJsonAsync("/api/v1/cart/update-quantity", request);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Kuantitas berhasil diperbarui.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return (false, "Item tidak ditemukan di keranjang.");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> RemoveFromCartAsync(string userId, string cartId)
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync($"/api/v1/cart/remove/{userId}/{cartId}");

            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return (true, "Item berhasil dihapus dari keranjang.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return (false, "Item tidak ditemukan di keranjang.");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}");
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

    public async Task<(bool Success, string Message, string? TransactionId)> CheckoutAsync(string userId, string paymentId)
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
                IdPayment = paymentId
            };

            var response = await _httpClient.PostAsJsonAsync("/api/v1/cart/checkout", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CheckoutResponse>();
                return (true, result?.Message ?? "Checkout berhasil!", result?.TransactionId);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                errorMessage = errorMessage.Trim('"');
                return (false, errorMessage, null);
            }

            var error = await response.Content.ReadAsStringAsync();
            return (false, error, null);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}", null);
        }
    }
}
