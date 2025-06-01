using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Genres.Queries.GetAllGenres;

public class GetAllGenresQueryValidator : AbstractValidator<GetAllGenresQuery>
{
    private readonly string[] allowedSortColumns = { "name" };

    public GetAllGenresQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}
