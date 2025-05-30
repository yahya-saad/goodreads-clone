using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.AuthorClaims.Commands.RequestAuthorClaim;
public class RequestAuthorClaimCommandHandler : IRequestHandler<RequestAuthorClaimCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<RequestAuthorClaimCommandHandler> _logger;

    public RequestAuthorClaimCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, ILogger<RequestAuthorClaimCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(RequestAuthorClaimCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
        {
            _logger.LogWarning("Unauthenticated user attempted to request author claim.");
            return Result.Fail(AuthErrors.Unauthorized);
        }
        _logger.LogInformation("User {UserId} is requesting claim for author {AuthorId}", userId, request.AuthorId);

        var (existingRequests, _) = await _unitOfWork.AuthorClaimRequests.GetAllAsync(
            filter: r => r.UserId == userId);

        if (existingRequests.Any())
        {
            var status = existingRequests.First().Status;
            _logger.LogWarning("User {UserId} already has a claim request with status {Status}.", userId, status);
            return Result.Fail(AuthorClaimRequestErrors.ExistRequestWithStatus(status));
        }

        var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
        if (author == null)
        {
            _logger.LogWarning("Author not found: {AuthorId}", request.AuthorId);
            return Result.Fail(AuthorErrors.NotFound(request.AuthorId));
        }

        if (author.IsClaimed)
            return Result.Fail(AuthorClaimRequestErrors.AuthorAlreadyClaimed);

        var claimRequest = new AuthorClaimRequest
        {
            AuthorId = request.AuthorId,
            UserId = userId,
        };

        await _unitOfWork.AuthorClaimRequests.AddAsync(claimRequest);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Successfully created author claim request for user {UserId} and author {AuthorId}", userId, request.AuthorId);
        return Result.Ok();
    }
}
