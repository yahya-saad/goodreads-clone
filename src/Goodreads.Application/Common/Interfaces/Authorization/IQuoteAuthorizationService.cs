namespace Goodreads.Application.Common.Interfaces.Authorization;
public interface IQuoteAuthorizationService
{
    Task<bool> IsOwnerOrAdminAsync(string quoteId);
}
