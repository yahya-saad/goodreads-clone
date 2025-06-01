namespace Goodreads.Application.Users.Queries.GetAllUsers;
public record GetAllUsersQuery(QueryParameters Parameters) : IRequest<PagedResult<UserDto>>;
