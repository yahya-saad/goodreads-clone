using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Quotes.Commands.ToggleQuoteLike;
public class ToggleQuoteLikeCommandHandler : IRequestHandler<ToggleQuoteLikeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<ToggleQuoteLikeCommandHandler> _logger;

    public ToggleQuoteLikeCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<ToggleQuoteLikeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(ToggleQuoteLikeCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result<bool>.Fail(AuthErrors.Unauthorized);

        var quote = await _unitOfWork.Quotes.GetByIdAsync(request.QuoteId);
        if (quote == null)
            return Result<bool>.Fail(QuoteErrors.NotFound(request.QuoteId));

        var existingLike = await _unitOfWork.QuoteLikes
            .GetSingleOrDefaultAsync(filter: q => q.QuoteId == request.QuoteId && q.UserId == userId);


        if (existingLike != null)
        {
            // Unlike
            _unitOfWork.QuoteLikes.Delete(existingLike);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User {UserId} unliked quote {QuoteId}", userId, request.QuoteId);
            return Result<bool>.Ok(false);
        }
        else
        {
            // Like
            var newLike = new QuoteLike { QuoteId = request.QuoteId, UserId = userId };
            await _unitOfWork.QuoteLikes.AddAsync(newLike);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User {UserId} liked quote {QuoteId}", userId, request.QuoteId);
            return Result<bool>.Ok(true);
        }
    }
}
