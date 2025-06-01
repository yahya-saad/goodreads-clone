namespace Goodreads.Application.Shelves.Commands.AddBookToShelf;
public class AddBookToShelfCommandValidator : AbstractValidator<AddBookToShelfCommand>
{
    public AddBookToShelfCommandValidator()
    {
        RuleFor(x => x.ShelfId).NotEmpty();
        RuleFor(x => x.BookId).NotEmpty();
    }
}