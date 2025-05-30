namespace Goodreads.Application.Common.Interfaces.Authorization;
public interface IAuthorAuthorizationService
{
    Task<bool> IsAuthorOwnerOrAdminAsync(string authorId);
}
