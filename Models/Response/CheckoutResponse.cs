using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class CheckoutResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; } = string.Empty;
}
