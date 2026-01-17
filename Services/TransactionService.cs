using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Request;
using BelanjaYuk.Client.Models.Response;

namespace BelanjaYuk.Client.Services;

public class TransactionService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public TransactionService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<TransactionHistoryResponse>> GetTransactionHistoryAsync(string userId, DateTime? fromDate = null, DateTime? toDate = null)
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"/api/v1/orders/cart/{userId}";

            // Build query string for date filters
            var queryParams = new List<string>();
            if (fromDate.HasValue)
            {
                queryParams.Add($"startDate={fromDate.Value:yyyy-MM-dd}");
            }
            if (toDate.HasValue)
            {
                queryParams.Add($"endDate={toDate.Value:yyyy-MM-dd}");
            }
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var transactions = await response.Content.ReadFromJsonAsync<List<TransactionHistoryResponse>>();
                return transactions ?? new List<TransactionHistoryResponse>();
            }

            return new List<TransactionHistoryResponse>();
        }
        catch
        {
            return new List<TransactionHistoryResponse>();
        }
    }

    public async Task<(bool Success, string Message)> SubmitReviewAsync(ReviewRequest request)
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync("/api/v1/orders/review", request);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Ulasan berhasil dikirim.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return (false, "Detail pesanan tidak ditemukan.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return (false, "Anda tidak bisa mengulas barang milik orang lain.");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage = errorMessage.Trim('"');
            return (false, errorMessage);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> DeleteReviewAsync(string userId, string productId)
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.DeleteAsync($"/api/v1/review/delete/{userId}/{productId}");

            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return (true, "Ulasan berhasil dihapus.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return (false, "Ulasan tidak ditemukan.");
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
