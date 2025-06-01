namespace Goodreads.Application.Reviews.Commands.DeleteReview;
public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<DeleteReviewCommandHandler> _logger;

    public DeleteReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<DeleteReviewCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var review = await _unitOfWork.BookReviews.GetByIdAsync(request.ReviewId);
        if (review == null)
            return Result.Fail(BookReviewErrors.NotFound(request.ReviewId));

        _unitOfWork.BookReviews.Delete(review);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("User {UserId} deleted review {ReviewId}", userId, request.ReviewId);

        return Result.Ok();
    }
}
