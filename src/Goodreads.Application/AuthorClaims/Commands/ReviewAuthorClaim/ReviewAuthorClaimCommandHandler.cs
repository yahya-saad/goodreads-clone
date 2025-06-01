namespace Goodreads.Application.AuthorClaims.Commands.ReviewAuthorClaim;
public class ReviewAuthorClaimHandler : IRequestHandler<ReviewAuthorClaimCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<ReviewAuthorClaimHandler> _logger;

    public ReviewAuthorClaimHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<ReviewAuthorClaimHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(ReviewAuthorClaimCommand request, CancellationToken cancellationToken)
    {
        var adminId = _userContext.UserId;
        if (adminId == null)
        {
            _logger.LogWarning("Unauthenticated user attempted to review author claim request.");
            return Result.Fail(AuthErrors.Unauthorized);
        }

        _logger.LogInformation("Admin {AdminId} is reviewing author claim request {RequestId}", adminId, request.RequestId);

        var claim = await _unitOfWork.AuthorClaimRequests.GetByIdAsync(request.RequestId, "Author", "User");

        if (claim == null || claim.Status != ClaimRequestStatus.Pending)
        {
            return Result.Fail(AuthorClaimRequestErrors.InvalidOrAlreadyReviewedRequest);
        }

        claim.Status = request.Approve ? ClaimRequestStatus.Approved : ClaimRequestStatus.Rejected;
        claim.ReviewedAt = DateTime.UtcNow;
        claim.ReviewedByAdminId = adminId;

        if (request.Approve)
        {
            claim.Author.UserId = claim.UserId;
            claim.Author.ClaimedAt = DateTime.UtcNow;
            claim.User.ClaimedAuthorProfile = claim.Author;
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}

