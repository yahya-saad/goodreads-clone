namespace Goodreads.Application.Auth.Commands.RefreshToken;
public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthResultDto>>;
