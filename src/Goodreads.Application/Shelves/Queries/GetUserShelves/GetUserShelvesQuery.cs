using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Shelves.Queries.GetUserShelves;
public record GetUserShelvesQuery(string UserId, QueryParameters Parameters) : IRequest<PagedResult<ShelfDto>>;