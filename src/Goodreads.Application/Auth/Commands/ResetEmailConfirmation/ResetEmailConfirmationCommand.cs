namespace Goodreads.Application.Auth.Commands.ResetEmailConfirmation;
public record ResetEmailConfirmationCommand(string email) : IRequest<Result<string>>;
