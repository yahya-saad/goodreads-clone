
using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Users.Commands.DeleteProfilePicture;
internal class DeleteProfilePictureCommandHandler : IRequestHandler<DeleteProfilePictureCommand, Result>
{
    private readonly ILogger<DeleteProfilePictureCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly UserManager<User> _userManager;
    private readonly IBlobStorageService _blobStorageService;

    public DeleteProfilePictureCommandHandler(
        ILogger<DeleteProfilePictureCommandHandler> logger,
        IUserContext userContext,
        UserManager<User> userManager,
        IBlobStorageService blobStorageService)
    {
        _logger = logger;
        _userContext = userContext;
        _userManager = userManager;
        _blobStorageService = blobStorageService;
    }
    public async Task<Result> Handle(DeleteProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        _logger.LogInformation("Deleting Profile Picture user with Id : {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result.Fail(UserErrors.NotFound(userId));
        }

        await _blobStorageService.DeleteAsync(user.ProfilePictureBlobName);

        user.ProfilePictureBlobName = null;
        user.ProfilePictureUrl = null;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            _logger.LogError("Failed to update profile picture for user: {UserId}. Errors: {Errors}", userId, result.Errors);
            return Result.Fail(UserErrors.UpdateFailed(userId));
        }

        return Result.Ok();
    }
}
