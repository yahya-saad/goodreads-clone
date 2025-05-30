namespace Goodreads.Domain.Entities;
public class BookGenre
{
    public string BookId { get; set; }
    public Book Book { get; set; }

    public string GenreId { get; set; }
    public Genre Genre { get; set; }
}

