namespace Goodreads.Application.Books.Commands.UpdateBookStatus;
public record UpdateBookStatusCommand(string BookId, string? TargetShelfName) : IRequest<Result>;
