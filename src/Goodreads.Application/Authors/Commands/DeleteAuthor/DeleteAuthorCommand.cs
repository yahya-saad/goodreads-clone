namespace Goodreads.Application.Authors.Commands.DeleteAuthor;
public record DeleteAuthorCommand(string Id) : IRequest<Result>;

