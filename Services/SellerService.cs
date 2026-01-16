using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Request;
using BelanjaYuk.Client.Models.Response;

namespace BelanjaYuk.Client.Services;

public class SellerService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public SellerService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<AuthResult<SellerRegisterResponse>> RegisterSellerAsync(SellerRegisterRequest request)
    {
        try
        {
            // Set Authorization header
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync("/api/v1/seller/register", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SellerRegisterResponse>();

                if (result != null)
                {
                    return new AuthResult<SellerRegisterResponse>
                    {
                        Success = true,
                        Message = "Toko berhasil didaftarkan!",
                        Data = result
                    };
                }

                return new AuthResult<SellerRegisterResponse>
                {
                    Success = false,
                    Message = "Response tidak valid dari server"
                };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            errorContent = errorContent.Trim('"');

            return new AuthResult<SellerRegisterResponse>
            {
                Success = false,
                Message = errorContent
            };
        }
        catch (HttpRequestException)
        {
            return new AuthResult<SellerRegisterResponse>
            {
                Success = false,
                Message = "Tidak dapat terhubung ke server. Periksa koneksi internet Anda."
            };
        }
        catch (Exception ex)
        {
            return new AuthResult<SellerRegisterResponse>
            {
                Success = false,
                Message = $"Terjadi kesalahan: {ex.Message}"
            };
        }
    }

    public async Task<List<MyProductResponse>> GetMyProductsAsync(string userId, string searchQuery = "")
    {
        try
        {
            // Set Authorization header
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var url = $"/api/v1/seller/my-products/{userId}";

            if (!string.IsNullOrWhiteSpace(searchQuery) && searchQuery.Length >= 2)
            {
                url += $"?searchQuery={Uri.EscapeDataString(searchQuery)}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<List<MyProductResponse>>(json, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return result ?? new List<MyProductResponse>();
            }

            return new List<MyProductResponse>();
        }
        catch
        {
            return new List<MyProductResponse>();
        }
    }
}
