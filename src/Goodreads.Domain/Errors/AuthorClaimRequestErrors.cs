using SharedKernel;

namespace Goodreads.Domain.Errors;

public static class AuthorClaimRequestErrors
{
    public static Error AuthorAlreadyClaimed => Error.Conflict(
        "AuthorClaimRequest.AlreadyClaimed",
        "Author already claimed."
    );


    public static Error InvalidOrAlreadyReviewedRequest => Error.Conflict(
        "AuthorClaimRequest.InvalidOrAlreadyReviewed",
        "Invalid or already reviewed request."
    );

    public static Error ExistRequestWithStatus(ClaimRequestStatus status)
    {
        return Error.Conflict(
            "AuthorClaimRequest.Exists",
            $"You already have an author claim request with status: {status}."
        );
    }
}



