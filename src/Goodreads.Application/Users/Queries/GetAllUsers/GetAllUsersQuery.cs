using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Users.Queries.GetAllUsers;
public record GetAllUsersQuery(QueryParameters Parameters) : IRequest<PagedResult<UserDto>>;
