namespace Goodreads.Application.UserFollows.Queries.GetFollowers;
public record GetFollowersQuery(int? PageNumber, int? PageSize) : IRequest<Result<PagedResult<UserDto>>>;

