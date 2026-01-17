using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response
{
    public class MyProductResponse
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

        [JsonPropertyName("qty")]
        public int Qty { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; } = new List<string>();
    }
}
