using FluentValidation;

namespace Goodreads.Application.UserYearChallenges.Commands.UpdateUserYearChallenge;

public class UpsertUserYearChallengeCommandValidator : AbstractValidator<UpsertUserYearChallengeCommand>
{
    public UpsertUserYearChallengeCommandValidator()
    {
        RuleFor(x => x.TargetBooksCount)
            .GreaterThan(0)
            .WithMessage("Target books count must be greater than zero.");
    }
}