
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Auth.Commands.LoginUser;
internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<User>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<LoginUserCommandHandler> _logger;
    public LoginUserCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginUserCommandHandler> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }



    public async Task<Result<User>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user == null)
            user = await _userManager.FindByNameAsync(request.UsernameOrEmail);

        if (user == null)
            return Result<User>.Fail("Invalid username or password.");

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (!result.Succeeded)
            return Result<User>.Fail("Invalid username or password.");

        _logger.LogInformation("User {Email} logged in successfully.", request.UsernameOrEmail);
        return Result<User>.Ok(user);
    }
}