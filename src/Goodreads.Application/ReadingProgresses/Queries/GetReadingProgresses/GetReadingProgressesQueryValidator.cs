using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.ReadingProgresses.Queries.GetReadingProgresses;
public class GetReadingProgressesQueryValidator : AbstractValidator<GetReadingProgressesQuery>
{
    private readonly string[] allowedSortColumns = { "currentpage", "updatedat" };

    public GetReadingProgressesQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}