using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Quotes.Commands.DeleteQuote;
public record DeleteQuoteCommand(string QuoteId) : IRequest<Result>, IRequireQuoteAuthorization;
