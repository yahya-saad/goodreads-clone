namespace Goodreads.Application.UserFollows.Commands.UnfollowUser;
public record UnfollowUserCommand(string FollowingId) : IRequest<Result>;

