using FluentValidation;
using Goodreads.Application.Common.Interfaces;

namespace Goodreads.Application.Quotes.Commands.CreateQuote;
public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuoteCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text cannot be empty")
            .MaximumLength(500)
            .WithMessage("Text must not exceed 500 characters");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required")
            .MustAsync(AuthorExists)
            .WithMessage("Author does not exist");

        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("BookId is required")
            .MustAsync(BookExists)
            .WithMessage("Book does not exist");

        RuleForEach(x => x.Tags)
            .MaximumLength(50)
            .WithMessage("Each tag must not exceed 50 characters");
    }

    private async Task<bool> AuthorExists(string authorId, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(authorId);
        return author != null ? true : false;
    }

    private async Task<bool> BookExists(string bookId, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId);
        return book != null ? true : false;
    }
}
