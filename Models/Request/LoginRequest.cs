using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Request;

public class LoginRequest
{
    [JsonPropertyName("emailOrPhone")]
    public string EmailOrPhone { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
