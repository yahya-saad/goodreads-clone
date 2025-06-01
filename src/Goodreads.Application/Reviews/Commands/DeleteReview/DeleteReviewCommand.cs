using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Reviews.Commands.DeleteReview;
public record DeleteReviewCommand(string ReviewId) : IRequest<Result>, IRequireReviewAuthorization;
