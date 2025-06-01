using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Reviews.Queries.GetAllReviews;
public class GetAllReviewsQueryValidator : AbstractValidator<GetAllReviewsQuery>
{
    private readonly string[] allowedSortColumns = { "createdat", "rating" };
    public GetAllReviewsQueryValidator()
    {
        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");

        RuleFor(x => x.UserId)
            .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
            .WithMessage("UserId must be a valid GUID");

        RuleFor(x => x.Bookid)
           .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
           .WithMessage("Bookid must be a valid GUID");
    }
}
