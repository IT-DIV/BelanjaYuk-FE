using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class CategoryResponse
{
    [JsonPropertyName("idCategory")]
    public string IdCategory { get; set; } = string.Empty;

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; } = string.Empty;
}
