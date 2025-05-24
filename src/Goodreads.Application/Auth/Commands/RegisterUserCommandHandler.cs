using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Auth.Commands;
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
            return Result<string>.Fail("Username is already taken.");

        if (await _userManager.FindByEmailAsync(request.Email) != null)
            return Result<string>.Fail("Email is already registered.");


        var user = _mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return Result<string>.Ok(user.Id.ToString());
        }

        return Result<string>.Fail(result.Errors.First().Description);
    }
}
