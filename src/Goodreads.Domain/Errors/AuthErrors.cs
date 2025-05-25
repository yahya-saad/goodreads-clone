using SharedKernel;

namespace Goodreads.Domain.Errors;
public static class AuthErrors
{
    public static Error InvalidCredentials() => Error.Unauthorized(
        "Auth.InvalidCredentials",
        "Invalid username or password.");

    public static Error Unauthenticated() => Error.Unauthorized(
        "Auth.Unauthenticated",
        "You must be logged in to access this resource.");

    public static Error Unauthorized() => Error.Forbidden(
        "Auth.Unauthorized",
        "You do not have permission to perform this action.");

    public static Error InvalidToken() => Error.Validation(
        "Auth.InvalidToken",
        "The token is invalid.");


}
