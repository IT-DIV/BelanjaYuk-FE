using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response
{
    public class ProductImageResponse
    {
        [JsonPropertyName("idProductImage")]
        public string IdProductImage { get; set; } = string.Empty;

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class CreateProductResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("product")]
        public ProductDetails Product { get; set; } = new ProductDetails();

        [JsonPropertyName("images")]
        public List<ProductImageResponse>? Images { get; set; }

        [JsonPropertyName("imageErrors")]
        public List<string>? ImageErrors { get; set; }
    }

    public class ProductDetails
    {
        [JsonPropertyName("idProduct")]
        public string IdProduct { get; set; } = string.Empty;

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("productDesc")]
        public string ProductDesc { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("discountProduct")]
        public decimal DiscountProduct { get; set; }

        [JsonPropertyName("qty")]
        public int Qty { get; set; }

        [JsonPropertyName("idCategory")]
        public string IdCategory { get; set; } = string.Empty;
    }
}
