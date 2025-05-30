namespace Goodreads.Application.Genres.Commands.DeleteGenre;
public record DeleteGenreCommand(string Id) : IRequest<Result>;

