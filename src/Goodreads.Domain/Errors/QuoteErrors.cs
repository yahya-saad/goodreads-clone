using SharedKernel;

namespace Goodreads.Domain.Errors;
public static class QuoteErrors
{
    public static Error NotFound(string id) => Error.NotFound(
    "Quote.NotFound",
    $"The quote with id '{id}' was not found.");
}
