using FluentValidation;

namespace Goodreads.Application.Books.Commands.RemoveGenreFromBook;
public class RemoveGenreFromBookCommandValidator : AbstractValidator<RemoveGenreFromBookCommand>
{
    public RemoveGenreFromBookCommandValidator()
    {
        RuleFor(x => x.BookId).NotEmpty();

        RuleFor(x => x.GenreId).NotEmpty();
    }
}
