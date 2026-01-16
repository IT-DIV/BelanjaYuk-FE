using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("idUser")]
    public string IdUser { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("storeName")]
    public string StoreName { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new List<string>();

    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }
}
