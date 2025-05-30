using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Shelves.Commands.DeleteShelf;
public record DeleteShelfCommand(string ShelfId) : IRequest<Result>, IRequireShelfAuthorization;

