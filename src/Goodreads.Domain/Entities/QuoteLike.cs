namespace Goodreads.Domain.Entities;
public class QuoteLike
{
    public string QuoteId { get; set; } = null!;
    public Quote Quote { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public DateTime LikedAt { get; set; } = DateTime.UtcNow;
}
