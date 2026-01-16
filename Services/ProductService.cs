using System.Net.Http.Json;
using BelanjaYuk.Client.Models.Request;
using BelanjaYuk.Client.Models.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace BelanjaYuk.Client.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public ProductService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
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

    public async Task<(bool Success, string Message, CreateProductResponse? Product)> CreateProductAsync(
        string userId,
        string namaBarang,
        string deskripsiBarang,
        string idKategori,
        decimal harga,
        decimal diskon,
        int stok,
        List<IBrowserFile>? images = null)
    {
        try
        {
            // Create HttpRequestMessage to have more control
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/products");

            // Set Authorization header
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Create multipart form data
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(userId), "UserId");
            formData.Add(new StringContent(namaBarang), "NamaBarang");
            formData.Add(new StringContent(deskripsiBarang), "DeskripsiBarang");
            formData.Add(new StringContent(idKategori), "IdKategori");
            formData.Add(new StringContent(harga.ToString(System.Globalization.CultureInfo.InvariantCulture)), "Harga");
            formData.Add(new StringContent(diskon.ToString(System.Globalization.CultureInfo.InvariantCulture)), "Diskon");
            formData.Add(new StringContent(stok.ToString()), "Stok");

            // Add images if provided
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    // Read file to byte array
                    var buffer = new byte[image.Size];
                    using (var stream = image.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024))
                    {
                        await stream.ReadAsync(buffer, 0, (int)image.Size);
                    }

                    var content = new ByteArrayContent(buffer);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
                    formData.Add(content, "images", image.Name);
                }
            }

            request.Content = formData;
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<CreateProductResponse>();
                return (true, product?.Message ?? "Produk berhasil ditambahkan", product);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return (false, "Anda bukan seller terdaftar.", null);
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage, null);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}", null);
        }
    }

    public async Task<(bool Success, string Message)> DeleteProductAsync(string userId, string productId)
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

            var response = await _httpClient.DeleteAsync($"/api/v1/products/{userId}/{productId}");

            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return (true, "Produk berhasil dihapus");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return (false, "Produk tidak ditemukan.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return (false, "Anda tidak memiliki izin untuk menghapus produk ini.");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message, CreateProductResponse? Product)> UpdateProductAsync(
        string productId,
        string userId,
        string namaBarang,
        string deskripsiBarang,
        string idKategori,
        decimal harga,
        decimal diskon,
        int stok,
        List<IBrowserFile>? images = null)
    {
        try
        {
            // Create HttpRequestMessage to have more control
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/products/update/{productId}");

            // Set Authorization header
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Create multipart form data
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(userId), "UserId");
            formData.Add(new StringContent(namaBarang), "NamaBarang");
            formData.Add(new StringContent(deskripsiBarang), "DeskripsiBarang");
            formData.Add(new StringContent(idKategori), "IdKategori");
            formData.Add(new StringContent(harga.ToString(System.Globalization.CultureInfo.InvariantCulture)), "Harga");
            formData.Add(new StringContent(diskon.ToString(System.Globalization.CultureInfo.InvariantCulture)), "Diskon");
            formData.Add(new StringContent(stok.ToString()), "Stok");

            // Add images if provided
            if (images != null && images.Count > 0)
            {
                foreach (var image in images)
                {
                    // Read file to byte array
                    var buffer = new byte[image.Size];
                    using (var stream = image.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024))
                    {
                        await stream.ReadAsync(buffer, 0, (int)image.Size);
                    }

                    var content = new ByteArrayContent(buffer);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
                    formData.Add(content, "images", image.Name);
                }
            }

            request.Content = formData;
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<CreateProductResponse>();
                return (true, product?.Message ?? "Produk berhasil diupdate", product);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return (false, "Produk tidak ditemukan.", null);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return (false, "Anda tidak memiliki izin untuk mengubah produk ini.", null);
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage, null);
        }
        catch (Exception ex)
        {
            return (false, $"Terjadi kesalahan: {ex.Message}", null);
        }
    }
}
