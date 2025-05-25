using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Goodreads.Application.Auth.Commands.ForgotPassword;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;

    public ForgotPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<string>.Fail(UserErrors.NotFound(request.Email));
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebUtility.UrlEncode(token);

        var resetLink = $"https://localhost:7257/api/auth/reset-password?userId={user.Id}&token={encodedToken}";

        /*        await _emailSender.SendEmailAsync(
                    request.Email,
                    "Reset your password",
                    $"Click this link to reset your password: {resetLink}");*/

        return Result<string>.Ok(resetLink);
    }
}
