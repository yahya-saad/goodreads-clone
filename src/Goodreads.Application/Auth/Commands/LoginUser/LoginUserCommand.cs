using Goodreads.Application.DTOs;

namespace Goodreads.Application.Auth.Commands.LoginUser;
public record LoginUserCommand(string UsernameOrEmail, string Password) : IRequest<Result<AuthResultDto>>;