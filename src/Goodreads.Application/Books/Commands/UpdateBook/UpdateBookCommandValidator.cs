namespace Goodreads.Application.Books.Commands.UpdateBook;
public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Book ID is required.");

        When(x => x.Title is not null, () =>
        {
            RuleFor(x => x.Title)
                .MaximumLength(200);
        });

        When(x => x.Description is not null, () =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(2000);
        });

        When(x => x.ISBN is not null, () =>
        {
            RuleFor(x => x.ISBN)
                .MaximumLength(17);
        });

        When(x => x.PublicationDate is not null, () =>
        {
            RuleFor(x => x.PublicationDate!.Value)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("PublicationDate cannot be in the future");
        });

        When(x => x.Language is not null, () =>
        {
            RuleFor(x => x.Language)
                .MaximumLength(50);
        });

        When(x => x.PageCount is not null, () =>
        {
            RuleFor(x => x.PageCount!.Value)
                .GreaterThan(0);
        });

        When(x => x.Publisher is not null, () =>
        {
            RuleFor(x => x.Publisher)
                .MaximumLength(100);
        });

    }
}
