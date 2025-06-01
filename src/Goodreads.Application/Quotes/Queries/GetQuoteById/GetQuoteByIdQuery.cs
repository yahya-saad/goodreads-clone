namespace Goodreads.Application.Quotes.Queries.GetQuoteById;
public record GetQuoteByIdQuery(string Id) : IRequest<Result<QuoteDto>>;