using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class TransactionHistoryResponse
{
    [JsonPropertyName("idBuyerTransaction")]
    public string IdBuyerTransaction { get; set; } = string.Empty;

    [JsonPropertyName("transactionDate")]
    public DateTime TransactionDate { get; set; }

    [JsonPropertyName("finalPrice")]
    public decimal FinalPrice { get; set; }

    [JsonPropertyName("paymentName")]
    public string PaymentName { get; set; } = string.Empty;

    [JsonPropertyName("products")]
    public List<TransactionItemResponse> Products { get; set; } = new List<TransactionItemResponse>();
}

public class TransactionItemResponse
{
    [JsonPropertyName("idBuyerTransactionDetail")]
    public string IdBuyerTransactionDetail { get; set; } = string.Empty;

    [JsonPropertyName("idProduct")]
    public string IdProduct { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("qty")]
    public int Qty { get; set; }

    [JsonPropertyName("priceAtTransaction")]
    public decimal PriceAtTransaction { get; set; }

    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("ratingComment")]
    public string RatingComment { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = new List<string>();
}
