namespace Goodreads.Domain.Entities;
public class Book
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? CoverImageBlobName { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string Language { get; set; }
    public int PageCount { get; set; }
    public string Publisher { get; set; }
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }

    public string AuthorId { get; set; }
    public Author Author { get; set; }

    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    public ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();
}
