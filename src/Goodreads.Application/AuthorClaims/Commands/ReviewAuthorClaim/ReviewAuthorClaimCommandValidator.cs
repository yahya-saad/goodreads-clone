namespace Goodreads.Application.AuthorClaims.Commands.ReviewAuthorClaim;
public class ReviewAuthorClaimCommandValidator : AbstractValidator<ReviewAuthorClaimCommand>
{
    public ReviewAuthorClaimCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("RequestId is required.")
            .Must(BeValidGuid).WithMessage("Invalid request id format.");
    }

    private bool BeValidGuid(string requestId)
    {
        return Guid.TryParse(requestId, out _);
    }
}
