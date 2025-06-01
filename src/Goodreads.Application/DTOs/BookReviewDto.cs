namespace Goodreads.Application.DTOs;
public class BookReviewDto
{
    public string Id { get; set; }

    public string BookId { get; set; }
    public string BookTitle { get; set; }

    public string UserId { get; set; }

    public int Rating { get; set; }
    public string? ReviewText { get; set; }

    public DateTime CreatedAt { get; set; }
}
