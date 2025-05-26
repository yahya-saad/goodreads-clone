namespace Goodreads.Application.Common.Interfaces;
public interface IUserFollowRepository
{
    Task<bool> IsFollowingAsync(string followerId, string followingId);
    Task FollowAsync(string followerId, string followingId);
    Task UnfollowAsync(string followerId, string followingId);
    Task<List<User>> GetFollowersAsync(string userId, int? pageNumber, int? pageSize);
    Task<List<User>> GetFollowingAsync(string userId, int? pageNumber, int? pageSize);

    Task<int> GetFollowersCountAsync(string userId);
    Task<int> GetFollowingCountAsync(string userId);
}
