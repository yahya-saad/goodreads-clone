using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;
using SharedKernel;

namespace Goodreads.Application.Shelves.Commands.DeleteShelf;
internal class DeleteShelfCommandHandler : IRequestHandler<DeleteShelfCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteShelfCommandHandler> _logger;
    private readonly IUserContext _userContext;

    public DeleteShelfCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteShelfCommandHandler> logger,
        IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _userContext = userContext;
    }

    public async Task<Result> Handle(DeleteShelfCommand request, CancellationToken cancellationToken)
    {
        var shelfId = request.ShelfId;
        _logger.LogInformation("Deleting shelf with ID: {ShelfId}", shelfId);

        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        var shelf = await _unitOfWork.Shelves.GetByIdAsync(shelfId);
        if (shelf == null)
        {
            _logger.LogWarning("Shelf with ID: {ShelfId} not found", shelfId);
            return Result.Fail(ShelfErrors.NotFound(shelfId));
        }

        if (shelf.IsDefault)
        {
            _logger.LogWarning("Attempt to delete default shelf: {ShelfName}", shelf.Name);
            return Result.Fail(Error.Failure("Shelves.DefaultShelfDeleteDenied", $"Cannot delete default shelf '{shelf.Name}'."));
        }

        _unitOfWork.Shelves.Delete(shelf);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }

}
