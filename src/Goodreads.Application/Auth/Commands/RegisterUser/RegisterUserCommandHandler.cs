using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Goodreads.Application.Auth.Commands.RegisterUser;
internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper, ILogger<RegisterUserCommandHandler> logger)
    {
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RegisterUserCommand for user: {UserName}", request.UserName);

        if (await _userManager.FindByNameAsync(request.UserName) != null)
            return Result<string>.Fail(UserErrors.UsernameTaken());

        if (await _userManager.FindByEmailAsync(request.Email) != null)
            return Result<string>.Fail(UserErrors.EmailAlreadyRegistered());


        var user = _mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result<string>.Fail(UserErrors.CreateUserFailed(result.Errors.First().Description));

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebUtility.UrlEncode(token);

        var confirmationLink = $"https://localhost:7257/api/auth/confirm-email?userId={user.Id}&token={encodedToken}"; // Should be Frontend

        //     await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Click <a href='{confirmationLink}'>here</a> to confirm your email.");

        return Result<string>.Ok(confirmationLink);
    }
}
