using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.UserYearChallenges.Commands.UpdateUserYearChallenge;
public class UpsertUserYearChallengeCommandHandler : IRequestHandler<UpsertUserYearChallengeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpsertUserYearChallengeCommandHandler> _logger;

    public UpsertUserYearChallengeCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, ILogger<UpsertUserYearChallengeCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpsertUserYearChallengeCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        int currentYear = DateTime.UtcNow.Year;

        var existingChallenge = (await _unitOfWork.UserYearChallenges.GetAllAsync(
            filter: c => c.UserId == userId && c.Year == currentYear)).Items.FirstOrDefault();

        if (existingChallenge == null)
        {
            var newChallenge = new UserYearChallenge
            {
                UserId = userId,
                Year = currentYear,
                TargetBooksCount = request.TargetBooksCount,
            };

            await _unitOfWork.UserYearChallenges.AddAsync(newChallenge);
            _logger.LogInformation("Created new year challenge for user {UserId} year {Year}", userId, currentYear);
        }
        else
        {
            existingChallenge.TargetBooksCount = request.TargetBooksCount;
            _logger.LogInformation("Updated year challenge target for user {UserId} year {Year}", userId, currentYear);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
