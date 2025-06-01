using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Infrastructure.Authorization;
internal class ReviewAuthorizationService(IUnitOfWork unitOfWork, IUserContext userContext) : IReviewAuthorizationService
{
    public async Task<bool> Authorize(string reviewId)
    {
        var userId = userContext.UserId;
        if (string.IsNullOrEmpty(userId))
            return false;

        var review = await unitOfWork.BookReviews.GetByIdAsync(reviewId);

        if (review == null)
            return false;

        return review.UserId == userId;
    }
}
