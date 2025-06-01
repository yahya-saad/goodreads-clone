namespace Goodreads.Application.Books.Queries.GetBooksByGenre;
public record GetBooksByGenreQuery(QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
