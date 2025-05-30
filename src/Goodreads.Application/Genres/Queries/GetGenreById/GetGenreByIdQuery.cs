using Goodreads.Application.DTOs;

namespace Goodreads.Application.Genres.Queries.GetGenreById;
public record GetGenreByIdQuery(string Id) : IRequest<Result<GenreDto>>;
