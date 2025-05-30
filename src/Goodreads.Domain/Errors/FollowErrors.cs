using SharedKernel;

namespace Goodreads.Domain.Errors;
public static class FollowErrors
{
    public static Error UserNotFound(string userId) => Error.NotFound(
        "Follow.UserNotFound",
        $"User with ID '{userId}' was not found.");

    public static Error AlreadyFollowing(string followerId, string followingId) => Error.Conflict(
        "Follow.AlreadyFollowing",
        $"User '{followerId}' is already following user '{followingId}'.");

    public static Error NotFollowing(string followerId, string followingId) => Error.Conflict(
        "Follow.NotFollowing",
        $"User '{followerId}' is not following user '{followingId}'.");

    public static Error SelfFollowNotAllowed => Error.Failure(
        "Follow.SelfFollowNotAllowed",
        "You cannot follow yourself.");
}