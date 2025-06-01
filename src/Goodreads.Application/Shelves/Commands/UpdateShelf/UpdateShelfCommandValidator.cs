namespace Goodreads.Application.Shelves.Commands.UpdateShelf;
internal class UpdateShelfCommandValidator : AbstractValidator<UpdateShelfCommand>
{
    public UpdateShelfCommandValidator()
    {
        RuleFor(x => x.ShelfId)
            .NotEmpty()
            .WithMessage("ShelfId is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Shelf name is required.")
            .MaximumLength(100)
            .WithMessage("Shelf name must not exceed 100 characters.");
    }
}
