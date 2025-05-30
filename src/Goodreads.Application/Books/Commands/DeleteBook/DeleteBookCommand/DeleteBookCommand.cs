namespace Goodreads.Application.Books.Commands.DeleteBook.DeleteBookCommand;
public record DeleteBookCommand(string Id) : IRequest<Result>;
