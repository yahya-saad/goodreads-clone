namespace Goodreads.Application.Auth.Commands.ForgotPassword;
public record ForgotPasswordCommand(string Email) : IRequest<Result<string>>;

