using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Books.Commands.AddGenresToBook;
public record AddGenresToBookCommand(string BookId, List<string> GenreIds) : IRequest<Result>, IRequireBookAuthorization;

