namespace Goodreads.Application.Genres.Queries.GetAllGenres;
public record GetAllGenresQuery(QueryParameters Parameters) : IRequest<PagedResult<GenreDto>>;
