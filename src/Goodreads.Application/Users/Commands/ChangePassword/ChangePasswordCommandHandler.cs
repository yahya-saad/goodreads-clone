namespace Goodreads.Application.Users.Commands.ChangePassword;
internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly ILogger<ChangePasswordCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IUserContext _userContext;

    public ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> logger, UserManager<User> userManager, IUserContext userContext)
    {
        _logger = logger;
        _userManager = userManager;
        _userContext = userContext;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        _logger.LogInformation("Change Password for user: {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result.Fail(UserErrors.NotFound(userId));
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed to change password for user: {UserId}.", userId);
            return Result.Fail(Error.Failure("Users.ChangePasswordFailed", "Failed to change password"));
        }

        return Result.Ok();
    }
}
