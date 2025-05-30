using FluentValidation;

namespace Goodreads.Application.AuthorClaims.Commands.RequestAuthorClaim;
public class RequestAuthorClaimValidator : AbstractValidator<RequestAuthorClaimCommand>
{
    public RequestAuthorClaimValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty();
    }
}