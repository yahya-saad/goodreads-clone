using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.UserYearChallenges.Queries.GetAllUserYearChallenges;
public class GetAllUserYearChallengesQueryValidator : AbstractValidator<GetAllUserYearChallengesQuery>
{
    private readonly string[] allowedSortColumns = { "year", "completedbookscount", "targetbookscount" };

    public GetAllUserYearChallengesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(col => string.IsNullOrEmpty(col) || allowedSortColumns.Contains(col.ToLower()))
            .WithMessage($"Sort column must be one of: {string.Join(", ", allowedSortColumns)}");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .When(x => x.Year.HasValue)
            .WithMessage("Year must be a valid year.");
    }
}
