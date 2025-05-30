using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Shelves.Commands.RemoveBookFromShelf;
public record RemoveBookFromShelfCommand(string ShelfId, string BookId) : IRequest<Result>, IRequireShelfAuthorization;
