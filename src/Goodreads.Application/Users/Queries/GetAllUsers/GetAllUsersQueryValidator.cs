using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Users.Queries.GetAllUsers;
public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    private readonly string[] allowedSortColumns = { "username", "country" };

    public GetAllUsersQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}
