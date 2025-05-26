using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.UserFollows.Queries.GetFollowing;
public record GetFollowingQuery(int? PageNumber, int? PageSize) : IRequest<Result<PagedResult<UserDto>>>;
