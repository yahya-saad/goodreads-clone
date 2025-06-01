using System.Net;

namespace Goodreads.Application.Auth.Commands.ResetEmailConfirmation;
public class ResetEmailConfirmationCommandHandler : IRequestHandler<ResetEmailConfirmationCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public ResetEmailConfirmationCommandHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<Result<string>> Handle(ResetEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.email);
        if (user == null)
            return Result<string>.Fail(UserErrors.NotFound(request.email));

        if (user.EmailConfirmed)
            return Result<string>.Fail(UserErrors.EmailAlreadyConfirmed(user.Id));

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = WebUtility.UrlEncode(token);

        var confirmationLink = $"https://localhost:7257/api/auth/confirm-email?userId={user.Id}&token={encodedToken}";

        await _emailService.SendEmailAsync(
            user.Email, "Confirm your email",
            $"Click <a href='{confirmationLink}'>here</a> to confirm your email.");

        return Result<string>.Ok(confirmationLink);
    }
}

