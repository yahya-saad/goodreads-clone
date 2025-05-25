namespace Goodreads.Application.Auth.Commands.ResetPassword;
public record ResetPasswordCommand(string userId, string Token, string NewPassword) : IRequest<Result>;

