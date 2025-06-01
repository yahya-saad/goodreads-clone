namespace Goodreads.Application.Common.Validation;
public class QueryParametersValidator : AbstractValidator<QueryParameters>
{
    public QueryParametersValidator()
    {
        RuleFor(q => q.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0")
            .When(q => q.PageNumber != null);

        RuleFor(q => q.PageSize)
            .InclusiveBetween(1, 50).WithMessage("Page size must be between 1 and 50")
            .When(q => q.PageSize != null);

        RuleFor(q => q.SortOrder)
            .Must(order => string.IsNullOrEmpty(order) || order.Equals("asc", StringComparison.OrdinalIgnoreCase) || order.Equals("desc", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Sort order must be 'asc' or 'desc'");
    }
}