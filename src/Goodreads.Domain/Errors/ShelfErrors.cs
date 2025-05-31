using SharedKernel;

namespace Goodreads.Domain.Errors;
public class ShelfErrors
{
    public static Error NotFound(string id) => Error.NotFound(
       "Shelf.NotFound",
       $"The shelf with id '{id}' was not found.");
    public static Error AlreadyAdded => Error.Conflict(
       "Shelf.AlreadyAdded",
       $"Book already in shelf");

    public static Error DefaultShelfAddDenied(string name) => Error.Conflict(
        "Shelf.DefaultShelfAddDenied",
        "Cannot add book to the default shelf.");
}
