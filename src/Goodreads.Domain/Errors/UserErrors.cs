using SharedKernel;

namespace Goodreads.Domain.Errors;
public static class UserErrors
{
    public static Error UsernameTaken() => Error.Conflict(
        "Users.UsernameTaken",
        "Username is already taken.");

    public static Error EmailAlreadyRegistered() => Error.Conflict(
        "Users.EmailAlreadyRegistered",
        "Email is already registered.");

    public static Error CreateUserFailed(string description) => Error.Failure(
        "Users.CreateUserFailed",
        description);


    public static Error NotFound(string identifier) => Error.NotFound(
    "Users.NotFound",
    $"The user '{identifier}' was not found.");

    public static Error EmailAlreadyConfirmed(string userId) => Error.Failure(
        "Users.EmailAlreadyConfirmed",
        $"The email for user '{userId}' is already confirmed.");

}