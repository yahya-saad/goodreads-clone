using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Books.Commands.UpdateBook;
public class UpdateBookCommand : IRequest<Result>, IRequireAuthorAuthorization
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ISBN { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public string? Language { get; set; }
    public int? PageCount { get; set; }
    public string? Publisher { get; set; }

    public string? AuthorId { get; set; }
}
