using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Goodreads.Application.Auth.Commands.ForgotPassword;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
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

        await _emailService.SendEmailAsync(
            user.Email,
            "Reset your password",
            $"Click <a href='{resetLink}'>here</a> to reset your password.");

        return Result<string>.Ok(resetLink);
    }
}
