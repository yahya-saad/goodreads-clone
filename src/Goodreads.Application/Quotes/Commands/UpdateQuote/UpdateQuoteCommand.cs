using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Quotes.Commands.UpdateQuote;
public record UpdateQuoteCommand(string QuoteId, string Text, List<string>? Tags) : IRequest<Result>, IRequireQuoteAuthorization;