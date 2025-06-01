namespace Goodreads.Application.Books.Queries.GetAllBooks;
public record GetAllBooksQuery(QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
