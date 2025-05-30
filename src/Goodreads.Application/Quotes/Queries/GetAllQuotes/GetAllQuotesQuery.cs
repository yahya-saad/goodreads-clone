using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Quotes.Queries.GetAllQuotes;
public record GetAllQuotesQuery(QueryParameters Parameters, string? Tag, string? UserId, string? AuthorId, string? BookId)
    : IRequest<PagedResult<QuoteDto>>;