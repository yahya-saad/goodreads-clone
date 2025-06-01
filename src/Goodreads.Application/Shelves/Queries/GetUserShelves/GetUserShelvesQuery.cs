namespace Goodreads.Application.Shelves.Queries.GetUserShelves;
public record GetUserShelvesQuery(string UserId, QueryParameters Parameters, string? Shelf) : IRequest<PagedResult<ShelfDto>>;