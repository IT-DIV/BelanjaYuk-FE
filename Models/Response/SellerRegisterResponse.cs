using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class SellerRegisterResponse
{
    [JsonPropertyName("idUserSeller")]
    public string IdUserSeller { get; set; } = string.Empty;

    [JsonPropertyName("idUser")]
    public string IdUser { get; set; } = string.Empty;

    [JsonPropertyName("sellerName")]
    public string SellerName { get; set; } = string.Empty;

    [JsonPropertyName("sellerDesc")]
    public string SellerDesc { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("sellerCode")]
    public string SellerCode { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

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
}
