using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Shelves.Commands.CreateShelf;
internal class CreateShelfCommandHandler : IRequestHandler<CreateShelfCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IUserContext _userContext;
    private readonly ILogger<CreateShelfCommandHandler> _logger;

    public CreateShelfCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateShelfCommandHandler> logger, UserManager<User> userManager, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _userManager = userManager;
        _userContext = userContext;
    }
    public async Task<Result<string>> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new shelf with name: {ShelfName}", request.Name);

        var userId = _userContext.UserId;
        if (userId == null)
            return Result<string>.Fail(AuthErrors.Unauthorized);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result<string>.Fail(UserErrors.NotFound(userId));
        }

        var shelf = new Shelf { UserId = userId, Name = request.Name.ToLower() };

        await _unitOfWork.Shelves.AddAsync(shelf);
        await _unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(shelf.Id);
    }
}
