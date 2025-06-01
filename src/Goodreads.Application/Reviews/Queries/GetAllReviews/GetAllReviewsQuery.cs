namespace Goodreads.Application.Reviews.Queries.GetAllReviews;
public record GetAllReviewsQuery(QueryParameters Parameters, string? UserId, string? Bookid) : IRequest<PagedResult<BookReviewDto>>;