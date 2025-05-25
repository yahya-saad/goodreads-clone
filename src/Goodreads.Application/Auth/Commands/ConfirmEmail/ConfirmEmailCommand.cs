namespace Goodreads.Application.Auth.Commands.ConfirmEmail;
public record ConfirmEmailCommand(string UserId, string Token) : IRequest<Result<bool>>;