namespace BelanjaYuk.Client.Models.Request;

public class ReviewRequest
{
    public string UserId { get; set; } = string.Empty;
    public string IdBuyerTransactionDetail { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string RatingComment { get; set; } = string.Empty;
}
