namespace Goodreads.Application.Reviews.Commands.CreateBookReview;
public record CreateReviewCommand(string BookId, int Rating, string ReviewText) : IRequest<Result<string>>;
