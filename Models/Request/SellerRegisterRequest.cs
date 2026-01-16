using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Request;

public class SellerRegisterRequest
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("namaToko")]
    public string NamaToko { get; set; } = string.Empty;

    [JsonPropertyName("noHpToko")]
    public string? NoHpToko { get; set; }

    [JsonPropertyName("urlToko")]
    public string UrlToko { get; set; } = string.Empty;

    [JsonPropertyName("deskripsi")]
    public string Deskripsi { get; set; } = string.Empty;

    [JsonPropertyName("alamatLengkap")]
    public string AlamatLengkap { get; set; } = string.Empty;
}
