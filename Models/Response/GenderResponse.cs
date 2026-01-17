using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class GenderResponse
{
    [JsonPropertyName("idGender")]
    public string IdGender { get; set; } = string.Empty;

    [JsonPropertyName("genderName")]
    public string GenderName { get; set; } = string.Empty;
}
