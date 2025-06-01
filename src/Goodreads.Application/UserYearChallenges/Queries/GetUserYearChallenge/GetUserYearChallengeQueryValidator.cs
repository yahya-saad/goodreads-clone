namespace Goodreads.Application.UserYearChallenges.Queries.GetUserYearChallenge;

public class GetUserYearChallengeQueryValidator : AbstractValidator<GetUserYearChallengeQuery>
{
    public GetUserYearChallengeQueryValidator()
    {
        RuleFor(x => x.UserId)
           .NotEmpty()
           .WithMessage("User ID is required.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("Year must be a valid year.");
    }
}