namespace Goodreads.Application.AuthorClaims.Commands.RequestAuthorClaim;
public record RequestAuthorClaimCommand(string AuthorId) : IRequest<Result>;

