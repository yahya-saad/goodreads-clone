namespace Goodreads.Domain.Entities;
public class BookReview : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public User User { get; set; }

    public string BookId { get; set; }
    public Book Book { get; set; }

    public int Rating { get; set; }
    public string? ReviewText { get; set; }
}
