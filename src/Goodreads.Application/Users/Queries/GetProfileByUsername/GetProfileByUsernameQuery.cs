namespace Goodreads.Application.Users.Queries.GetProfileByUsername;
public record GetProfileByUsernameQuery(string Username) : IRequest<Result<UserProfileDto>>;
