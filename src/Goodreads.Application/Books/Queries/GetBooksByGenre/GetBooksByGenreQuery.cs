using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Books.Queries.GetBooksByGenre;
public record GetBooksByGenreQuery(QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
