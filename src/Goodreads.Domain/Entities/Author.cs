namespace Goodreads.Domain.Entities;
public class Author
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = default!;
    public string Bio { get; set; } = default!;
    public string? ProfilePictureUrl { get; set; } = default!;
    public string? ProfilePictureBlobName { get; set; } = default!;

    // claimed by user
    public string? UserId { get; set; }
    public User? User { get; set; }
    public DateTime? ClaimedAt { get; set; }

    public bool IsClaimed => UserId != null;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
