using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class VerifyTokenResponse
{
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }

    [JsonPropertyName("user")]
    public UserInfo? User { get; set; }
}

public class UserInfo
{
    [JsonPropertyName("idUser")]
    public string IdUser { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new List<string>();

    [JsonPropertyName("storeName")]
    public string? StoreName { get; set; }

    [JsonPropertyName("idUserSeller")]
    public string? IdUserSeller { get; set; }
}
