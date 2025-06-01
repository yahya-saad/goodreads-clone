namespace Goodreads.Application.Reviews.Commands.CreateBookReview;
public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId is required.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid BookId format.");


        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.ReviewText)
            .NotEmpty().WithMessage("Review text is required.")
            .MaximumLength(2500).WithMessage("Review text can't exceed 2500 characters.");
    }
}