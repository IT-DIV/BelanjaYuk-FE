using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class CartItemResponse
{
    [JsonPropertyName("idBuyerCart")]
    public string IdBuyerCart { get; set; } = string.Empty;

    [JsonPropertyName("idProduct")]
    public string IdProduct { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("qty")]
    public int Qty { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discountProduct")]
    public decimal DiscountProduct { get; set; }

    [JsonPropertyName("priceAfterDiscount")]
    public decimal PriceAfterDiscount { get; set; }

    [JsonPropertyName("subTotal")]
    public decimal SubTotal { get; set; }

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = new List<string>();
}
