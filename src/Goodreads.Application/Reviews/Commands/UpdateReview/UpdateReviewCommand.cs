using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Reviews.Commands.UpdateReview;
public record UpdateReviewCommand(string ReviewId, int? Rating, string? ReviewText)
    : IRequest<Result>, IRequireReviewAuthorization;

public record UpdateReviewRequest(int? Rating, string? ReviewText);
