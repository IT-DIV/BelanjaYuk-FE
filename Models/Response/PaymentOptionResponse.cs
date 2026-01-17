using System.Text.Json.Serialization;

namespace BelanjaYuk.Client.Models.Response;

public class PaymentOptionResponse
{
    [JsonPropertyName("idPayment")]
    public string IdPayment { get; set; } = string.Empty;

    [JsonPropertyName("paymentName")]
    public string PaymentName { get; set; } = string.Empty;
}
