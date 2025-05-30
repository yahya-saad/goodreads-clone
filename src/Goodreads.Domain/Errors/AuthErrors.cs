using SharedKernel;

namespace Goodreads.Domain.Errors;
public static class AuthErrors
{
    public static Error InvalidCredentials => Error.Unauthorized(
        "Auth.InvalidCredentials",
        "Invalid username or password.");

    public static Error Unauthorized => Error.Unauthorized(
        "Auth.Unauthorized",
        "You are not authenticated.");


    public static Error Forbidden => Error.Forbidden(
        "Auth.Forbidden",
        "You do not have permission to perform this action.");

    public static Error InvalidToken => Error.Failure(
        "Auth.InvalidToken",
        "The token is invalid.");


}
