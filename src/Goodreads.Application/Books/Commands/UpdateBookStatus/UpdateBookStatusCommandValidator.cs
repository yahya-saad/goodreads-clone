namespace Goodreads.Application.Books.Commands.UpdateBookStatus;
using FluentValidation;
using Goodreads.Application.Common.Interfaces;

public class UpdateBookStatusCommandValidator : AbstractValidator<UpdateBookStatusCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookStatusCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId must be provided")
            .MustAsync(BookExists).WithMessage("Book does not exist");

        RuleFor(x => x.TargetShelfName)
                   .Must(name => name == null || DefaultShelves.All.Contains(name))
                   .WithMessage($"TargetShelfName must be one of the default shelves or null. Allowed values: {string.Join(", ", DefaultShelves.All)}");
    }

    private async Task<bool> BookExists(string bookId, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId);
        return book != null;
    }
}

