namespace Goodreads.Application.Shelves.Commands.CreateShelf;
public record CreateShelfCommand(string Name) : IRequest<Result<string>>;


