namespace Goodreads.Application.UserFollows.Commands.FollowUser;
public record FollowUserCommand(string FollowingId) : IRequest<Result>;

