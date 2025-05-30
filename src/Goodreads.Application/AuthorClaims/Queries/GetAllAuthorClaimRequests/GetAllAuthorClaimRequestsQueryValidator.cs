using FluentValidation;

namespace Goodreads.Application.AuthorClaims.Queries.GetAllAuthorClaimRequests;
public class GetAllAuthorClaimRequestsQueryValidator : AbstractValidator<GetAllAuthorClaimRequestsQuery>
{
    private readonly string[] allowedSortColumns = { "authorid", "requestedat", "reviewedat" };
    public GetAllAuthorClaimRequestsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0")
            .When(x => x.PageNumber != null);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50).WithMessage("Page size must be between 1 and 50")
            .When(x => x.PageSize != null);

        RuleFor(x => x.SortOrder)
            .Must(order => string.IsNullOrEmpty(order) || order.Equals("asc", StringComparison.OrdinalIgnoreCase) || order.Equals("desc", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Sort order must be 'asc' or 'desc'");

        RuleFor(x => x.SortColumn)
           .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
           .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue)
            .WithMessage("Invalid claim status value mube be one of [Pending, Approved, Rejected].");
    }
}
