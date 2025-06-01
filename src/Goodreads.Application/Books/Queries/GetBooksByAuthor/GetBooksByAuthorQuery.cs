namespace Goodreads.Application.Books.Queries.GetBooksByAuthor;
public record GetBooksByAuthorQuery(string AuthorId, QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
