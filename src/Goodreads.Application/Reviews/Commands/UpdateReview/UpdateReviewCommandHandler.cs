namespace Goodreads.Application.Reviews.Commands.UpdateReview;
public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateReviewCommandHandler> _logger;

    public UpdateReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<UpdateReviewCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var review = await _unitOfWork.BookReviews.GetByIdAsync(request.ReviewId);

        if (review == null)
            return Result.Fail(BookReviewErrors.NotFound(request.ReviewId));

        if (request.Rating.HasValue)
            review.Rating = request.Rating.Value;

        if (request.ReviewText != null)
            review.ReviewText = request.ReviewText;

        _unitOfWork.BookReviews.Update(review);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {UserId} updated review {ReviewId}", userId, request.ReviewId);

        return Result.Ok();
    }
}
