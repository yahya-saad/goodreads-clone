namespace Goodreads.Application.DTOs;
public class BookDetailDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string Language { get; set; }
    public int PageCount { get; set; }
    public string Publisher { get; set; }
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }
    public AuthorDto Author { get; set; }
    public List<BookGenreDto> Genres { get; set; }
}

