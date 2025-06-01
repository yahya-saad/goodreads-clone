namespace Goodreads.Application.Books.Commands.AddGenresToBook;
public class AddGenresToBookCommandValidator : AbstractValidator<AddGenresToBookCommand>
{
    public AddGenresToBookCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty();

        RuleFor(x => x.GenreIds)
            .NotNull()
            .Must(g => g.Count > 0).WithMessage("At least one genre must be specified.");
    }
}
