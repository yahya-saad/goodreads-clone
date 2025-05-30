using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Books.Queries.GetAllBooks;
public record GetAllBooksQuery(QueryParameters Parameters) : IRequest<PagedResult<BookDto>>;
