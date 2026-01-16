using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class ProductResponse
{
    [JsonPropertyName("idProduct")]
    public string IdProduct { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discountProduct")]
    public decimal DiscountProduct { get; set; }

    [JsonPropertyName("priceAfterDiscount")]
    public decimal PriceAfterDiscount { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; } = string.Empty;

    [JsonPropertyName("avgRating")]
    public decimal AvgRating { get; set; }
}
