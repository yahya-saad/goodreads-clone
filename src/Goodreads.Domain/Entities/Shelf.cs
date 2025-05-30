namespace Goodreads.Domain.Entities;
public class Shelf : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsDefault { get; set; } = false;
    public ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();
}
