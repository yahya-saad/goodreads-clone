
namespace Goodreads.Application.UserFollows.Queries.GetFollowing;
public record GetFollowingQuery(int? PageNumber, int? PageSize) : IRequest<Result<PagedResult<UserDto>>>;
