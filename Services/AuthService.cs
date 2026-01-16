using System.Net.Http.Json;
using System.Text.Json;
using BelanjaYuk.Client.Models.Request;
using BelanjaYuk.Client.Models.Response;
using Microsoft.JSInterop;

namespace BelanjaYuk.Client.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private const string TOKEN_KEY = "authToken";

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<AuthResult<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/register", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();

                if (result != null)
                {
                    return new AuthResult<RegisterResponse>
                    {
                        Success = true,
                        Message = "Registrasi berhasil!",
                        Data = result
                    };
                }

                return new AuthResult<RegisterResponse>
                {
                    Success = false,
                    Message = "Response tidak valid dari server"
                };
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            errorContent = errorContent.Trim('"');

            return new AuthResult<RegisterResponse>
            {
                Success = false,
                Message = errorContent
            };
        }
        catch (HttpRequestException ex)
        {
            return new AuthResult<RegisterResponse>
            {
                Success = false,
                Message = "Tidak dapat terhubung ke server. Periksa koneksi internet Anda."
            };
        }
        catch (Exception ex)
        {
            return new AuthResult<RegisterResponse>
            {
                Success = false,
                Message = $"Terjadi kesalahan: {ex.Message}"
            };
        }
    }

    public async Task<AuthResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (result != null)
                {
                    // Save token to localStorage
                    await SetTokenAsync(result.Token);

                    return new AuthResult<LoginResponse>
                    {
                        Success = true,
                        Message = "Login berhasil!",
                        Data = result
                    };
                }

                return new AuthResult<LoginResponse>
                {
                    Success = false,
                    Message = "Response tidak valid dari server"
                };
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            errorContent = errorContent.Trim('"');

            return new AuthResult<LoginResponse>
            {
                Success = false,
                Message = errorContent
            };
        }
        catch (HttpRequestException ex)
        {
            return new AuthResult<LoginResponse>
            {
                Success = false,
                Message = "Tidak dapat terhubung ke server. Periksa koneksi internet Anda."
            };
        }
        catch (Exception ex)
        {
            return new AuthResult<LoginResponse>
            {
                Success = false,
                Message = $"Terjadi kesalahan: {ex.Message}"
            };
        }
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TOKEN_KEY);
        }
        catch
        {
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token);
    }

    public async Task RemoveTokenAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
}

public class AuthResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}
