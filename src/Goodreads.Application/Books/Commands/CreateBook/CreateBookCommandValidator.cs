namespace Goodreads.Application.Books.Commands.CreateBook;
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.ISBN)
            .NotEmpty()
            .MaximumLength(17);

        RuleFor(x => x.PublicationDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("PublicationDate cannot be in the future");

        RuleFor(x => x.Language)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.PageCount)
            .GreaterThan(0);

        RuleFor(x => x.Publisher)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.AuthorId)
            .NotEmpty();
    }
}
