
using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Users.Commands.UpdateSocials;
internal class UpdateSocialsCommandHandler : IRequestHandler<UpdateSocialsCommand, Result>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateSocialsCommandHandler> _logger;
    public UpdateSocialsCommandHandler(UserManager<User> userManager, IUserContext userContext, ILogger<UpdateSocialsCommandHandler> logger)
    {
        _userManager = userManager;
        _userContext = userContext;
        _logger = logger;
    }
    public async Task<Result> Handle(UpdateSocialsCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthenticated());

        _logger.LogInformation("Updating socials for user: {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result.Fail(UserErrors.NotFound(userId));
        }

        user.Social.Twitter = request.Twitter;
        user.Social.Facebook = request.Facebook;
        user.Social.Linkedin = request.LinkedIn;


        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to update socials for user: {UserId}. Errors: {Errors}", userId, result.Errors);
            return Result.Fail(UserErrors.UpdateFailed(userId));
        }

        _logger.LogInformation("Successfully updated socials for user: {UserId}", userId);

        return Result.Ok();
    }
}
