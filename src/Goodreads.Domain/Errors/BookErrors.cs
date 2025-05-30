using SharedKernel;

namespace Goodreads.Domain.Errors;
public class BookErrors
{
    public static Error NotFound(string id) => Error.NotFound(
    "Book.NotFound",
    $"The book with id '{id}' was not found.");
}
