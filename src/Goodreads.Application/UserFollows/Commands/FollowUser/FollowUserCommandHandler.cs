using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.UserFollows.Commands.FollowUser;
internal class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Result>
{
    private readonly ILogger<FollowUserCommandHandler> _logger;
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly IUserContext _userContext;
    private readonly UserManager<User> _userManager;
    public FollowUserCommandHandler(ILogger<FollowUserCommandHandler> logger, IUserFollowRepository userFollowRepository, IUserContext userContext, UserManager<User> userManager)
    {
        _logger = logger;
        _userFollowRepository = userFollowRepository;
        _userContext = userContext;
        _userManager = userManager;
    }
    public async Task<Result> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var followerId = _userContext.UserId;
        var followingId = request.FollowingId;
        _logger.LogInformation("User with Id : {FollowerId} following user with Id : {FollowingId}", followerId, followingId);

        if (followerId == null)
            return Result.Fail(AuthErrors.Unauthenticated());

        if (followerId == request.FollowingId)
            return Result.Fail(FollowErrors.SelfFollowNotAllowed());

        var follower = await _userManager.FindByIdAsync(followerId);
        if (follower == null)
            return Result.Fail(FollowErrors.UserNotFound(followerId));

        var following = await _userManager.FindByIdAsync(request.FollowingId);
        if (following is null)
            return Result.Fail(FollowErrors.UserNotFound(request.FollowingId));

        var isAlreadyFollowing = await _userFollowRepository.IsFollowingAsync(followerId, followingId);

        if (isAlreadyFollowing)
            return Result.Fail(FollowErrors.AlreadyFollowing(followerId, followingId));

        await _userFollowRepository.FollowAsync(followerId, followingId);

        return Result.Ok();
    }
}
