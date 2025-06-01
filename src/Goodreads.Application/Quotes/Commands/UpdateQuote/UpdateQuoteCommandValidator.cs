namespace Goodreads.Application.Quotes.Commands.UpdateQuote;
internal class UpdateQuoteCommandValidator : AbstractValidator<UpdateQuoteCommand>
{
    private readonly IUnitOfWork unitOfWork;
    public UpdateQuoteCommandValidator(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;

        RuleFor(x => x.QuoteId)
           .NotEmpty()
           .WithMessage("QuoteId is required")
           .MustAsync(QuoteExists)
            .WithMessage("Quote does not exist");


        RuleFor(x => x.Text)
           .NotEmpty().WithMessage("Text is required")
           .MaximumLength(500).WithMessage("Text cannot exceed 500 characters");

        RuleForEach(x => x.Tags ?? Enumerable.Empty<string>())
            .MaximumLength(50)
            .WithMessage("Each tag must not exceed 50 characters");
    }

    private async Task<bool> QuoteExists(string id, CancellationToken cancellationToken)
    {
        var quote = await unitOfWork.Quotes.GetByIdAsync(id);
        return quote != null;
    }
}
