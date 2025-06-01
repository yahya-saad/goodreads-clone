namespace Goodreads.Application.Common.Interfaces.Authorization;
public interface IReviewAuthorizationService
{
    public Task<bool> Authorize(string reviewId);
}
