using Goodreads.Application.DTOs;

namespace Goodreads.Application.Books.Queries.GetBookById;
public record GetBookByIdQuery(string Id) : IRequest<Result<BookDetailDto>>;
