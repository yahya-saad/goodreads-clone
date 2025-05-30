using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Books.Commands.RemoveGenreFromBook;
public record RemoveGenreFromBookCommand(string BookId, string GenreId) : IRequest<Result>, IRequireBookAuthorization;
