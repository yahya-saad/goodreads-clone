namespace Goodreads.Application.Common.Interfaces.Authorization;

public interface IShelfAuthorizationService
{
    Task<bool> IsOwnerAsync(string shelfId);
}