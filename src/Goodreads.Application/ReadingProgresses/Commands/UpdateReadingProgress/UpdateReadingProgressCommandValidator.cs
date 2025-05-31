using FluentValidation;
using Goodreads.Application.Common.Interfaces;

namespace Goodreads.Application.ReadingProgresses.Commands.UpdateReadingProgress;

public class UpdateReadingProgressCommandValidator : AbstractValidator<UpdateReadingProgressCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReadingProgressCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId is required.")
            .MustAsync(BookExists).WithMessage("Book does not exist");

        RuleFor(x => x.CurrentPage)
            .GreaterThanOrEqualTo(1).WithMessage("CurrentPage must be at least 1.")
            .MustAsync(ValidCurrentPage).WithMessage("CurrentPage must not exceed the book's total pages.");
    }

    private async Task<bool> BookExists(string bookId, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId);
        return book != null;
    }

    private async Task<bool> ValidCurrentPage(UpdateReadingProgressCommand command, int currentPage, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(command.BookId);
        return book != null && currentPage <= book.PageCount;
    }
}