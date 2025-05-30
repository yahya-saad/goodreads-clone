namespace Goodreads.Domain.Entities;
public class Genre
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public int BookCount { get; set; }

    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}

