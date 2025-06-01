namespace Goodreads.Application.Reviews.Queries.GetReviewById;
public record GetReviewByIdQuery(string ReviewId) : IRequest<Result<BookReviewDto>>;