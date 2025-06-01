namespace Goodreads.Application.Users.Commands.UpdateUserProfile;
internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateUserProfileCommandHandler> _logger;

    public UpdateUserProfileCommandHandler(IUserContext userContext, UserManager<User> userManager, ILogger<UpdateUserProfileCommandHandler> logger)
    {
        _userContext = userContext;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        _logger.LogInformation("Updating socials for user: {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result.Fail(UserErrors.NotFound(userId));
        }

        if (!string.IsNullOrWhiteSpace(request.FirstName))
            user.FirstName = request.FirstName;

        if (!string.IsNullOrWhiteSpace(request.LastName))
            user.LastName = request.LastName;

        if (!string.IsNullOrWhiteSpace(request.Bio))
            user.Bio = request.Bio;

        if (!string.IsNullOrWhiteSpace(request.WebsiteUrl))
            user.WebsiteUrl = request.WebsiteUrl;

        if (!string.IsNullOrWhiteSpace(request.Country))
            user.Country = request.Country;

        if (request.DateOfBirth.HasValue)
            user.DateOfBirth = request.DateOfBirth.Value;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to update profile for user: {UserId}. Errors: {Errors}", userId, result.Errors);
            return Result.Fail(UserErrors.UpdateFailed(userId));
        }

        _logger.LogInformation("Successfully updated profile for user: {UserId}", userId);

        return Result.Ok();
    }
}

