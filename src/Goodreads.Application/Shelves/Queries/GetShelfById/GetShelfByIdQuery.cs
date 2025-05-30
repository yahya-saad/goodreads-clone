using Goodreads.Application.DTOs;

namespace Goodreads.Application.Shelves.Queries.GetShelfById;
public record GetShelfByIdQuery(string Id) : IRequest<Result<ShelfDto>>;
