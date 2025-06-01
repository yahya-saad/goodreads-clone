using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Shelves.Queries.GetUserShelves;
public class GetUserShelvesQueryValidator : AbstractValidator<GetUserShelvesQuery>
{
    private readonly string[] allowedSortColumns = { "name" };
    public GetUserShelvesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}
