namespace Goodreads.Application.Reviews.Commands.UpdateReview;
public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("ReviewId is required.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid ReviewId format.");

        When(x => x.Rating.HasValue, () =>
        {
            RuleFor(x => x.Rating.Value)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        });

        When(x => x.ReviewText != null, () =>
        {
            RuleFor(x => x.ReviewText)
                .MaximumLength(2500).WithMessage("Review text can't exceed 2500 characters.");
        });
    }
}
