namespace Goodreads.Application.Genres.Commands.CreateGenre;
public record CreateGenreCommand(string Name) : IRequest<Result<string>>;


