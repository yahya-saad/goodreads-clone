namespace Goodreads.Application.DTOs;
public class ShelfDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public int BookCount { get; set; }
    public List<BookDto> Books { get; set; } = new();
}