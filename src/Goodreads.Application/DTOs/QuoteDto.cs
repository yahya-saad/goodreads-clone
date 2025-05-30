namespace Goodreads.Application.DTOs;
public class QuoteDto
{
    public string Id { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string AuthorId { get; set; } = null!;
    public string BookId { get; set; } = null!;
    public string CreatedByUserId { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int LikesCount { get; set; }
    public List<string> Tags { get; set; } = new();
}

