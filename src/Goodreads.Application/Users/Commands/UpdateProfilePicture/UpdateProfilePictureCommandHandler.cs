using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Constants;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Users.Commands.UpdateProfilePicture;
internal class UpdateProfilePictureCommandHandler : IRequestHandler<UpdateProfilePictureCommand, Result>
{
    private readonly ILogger<UpdateProfilePictureCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly UserManager<User> _userManager;
    private readonly IBlobStorageService _blobStorageService;

    public UpdateProfilePictureCommandHandler(
        ILogger<UpdateProfilePictureCommandHandler> logger,
        IUserContext userContext,
        UserManager<User> userManager,
        IBlobStorageService blobStorageService)
    {
        _logger = logger;
        _userContext = userContext;
        _userManager = userManager;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        _logger.LogInformation("Updating Profile Picture user with Id : {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result.Fail(UserErrors.NotFound(userId));
        }

        using var stream = request.File.OpenReadStream();
        var (url, blobName) = await _blobStorageService.UploadAsync(request.File.FileName, stream, BlobContainer.Users);

        user.ProfilePictureUrl = url;
        user.ProfilePictureBlobName = blobName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to update profile picture for user: {UserId}. Errors: {Errors}", userId, result.Errors);
            return Result.Fail(UserErrors.UpdateFailed(userId));
        }

        return Result.Ok();
    }
}
