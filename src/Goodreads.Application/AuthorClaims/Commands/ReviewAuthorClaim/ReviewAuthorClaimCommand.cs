namespace Goodreads.Application.AuthorClaims.Commands.ReviewAuthorClaim;
public record ReviewAuthorClaimCommand(string RequestId, bool Approve) : IRequest<Result>;

