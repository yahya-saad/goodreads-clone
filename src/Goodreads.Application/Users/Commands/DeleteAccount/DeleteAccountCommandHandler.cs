
using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Goodreads.Application.Users.Commands.DeleteAccount;
internal class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
{
    private readonly ILogger<DeleteAccountCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IUserContext _userContext;
    public DeleteAccountCommandHandler(ILogger<DeleteAccountCommandHandler> logger, UserManager<User> userManager, IUserContext userContext)
    {
        _logger = logger;
        _userManager = userManager;
        _userContext = userContext;
    }
    public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthenticated());

        _logger.LogInformation("Deleting user with Id : {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result.Fail(UserErrors.NotFound(userId));
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to delete user: {UserId}", userId);
            return Result.Fail(Error.Failure("Users.DeleteFailed", $"Failed to delete user '{userId}'"));
        }

        return Result.Ok();
    }
}
