namespace Goodreads.Domain.Entities;
public class ReadingProgress
{
    public string BookId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public int CurrentPage { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Book Book { get; set; } = default!;
}
