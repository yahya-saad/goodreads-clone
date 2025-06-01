namespace Goodreads.Application.Reviews.Queries.GetReviewById;
public class GetReviewByIdQueryValidator : AbstractValidator<GetReviewByIdQuery>
{
    public GetReviewByIdQueryValidator()
    {
        RuleFor(q => q.ReviewId)
            .NotEmpty().WithMessage("ReviewId is required.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid ReviewId format.");
    }
}
