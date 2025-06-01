using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Authors.Queries.GetAllAuthors;
public class GetAllAuthorsQueryValidator : AbstractValidator<GetAllAuthorsQuery>
{
    private readonly string[] allowedSortColumns = { "id", "name" };

    public GetAllAuthorsQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}
