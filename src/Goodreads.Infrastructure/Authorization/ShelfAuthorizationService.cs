using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Infrastructure.Authorization;
internal class ShelfAuthorizationService(IUnitOfWork unitOfWork, IUserContext userContext) : IShelfAuthorizationService
{
    public async Task<bool> IsOwnerAsync(string shelfId)
    {
        var userId = userContext.UserId;
        if (userId == null) return false;

        var shelf = await unitOfWork.Shelves.GetByIdAsync(shelfId);
        if (shelf == null) return false;

        return shelf.UserId == userId;
    }
}
