namespace Goodreads.Application.Reviews.Commands.DeleteReview;
public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("ReviewId is required.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid ReviewId format.");
    }
}
