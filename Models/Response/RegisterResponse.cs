using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class RegisterResponse
{
    [JsonPropertyName("idUser")]
    public string IdUser { get; set; } = string.Empty;

    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("dob")]
    public DateTime Dob { get; set; }

    [JsonPropertyName("dateIn")]
    public DateTime DateIn { get; set; }

    [JsonPropertyName("userIn")]
    public string UserIn { get; set; } = string.Empty;

    [JsonPropertyName("dateUp")]
    public DateTime DateUp { get; set; }

    [JsonPropertyName("userUp")]
    public string UserUp { get; set; } = string.Empty;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("idGender")]
    public string IdGender { get; set; } = string.Empty;
}
