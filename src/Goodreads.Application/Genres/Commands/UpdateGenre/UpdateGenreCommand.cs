namespace Goodreads.Application.Genres.Commands.UpdateGenre;
public record UpdateGenreCommand(string Id, string Name) : IRequest<Result>;
