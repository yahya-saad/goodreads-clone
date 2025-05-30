namespace Goodreads.Application.Quotes.Commands.ToggleQuoteLike;
public record ToggleQuoteLikeCommand(string QuoteId) : IRequest<Result<bool>>;
