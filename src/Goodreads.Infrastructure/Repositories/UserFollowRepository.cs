using Goodreads.Application.Common.Extensions;
using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Goodreads.Infrastructure.Repositories;
internal class UserFollowRepository(ApplicationDbContext context) : IUserFollowRepository
{
    public async Task<bool> IsFollowingAsync(string followerId, string followingId) =>
        await context.UserFollows.AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

    public async Task FollowAsync(string followerId, string followingId)
    {
        if (await IsFollowingAsync(followerId, followingId)) return;

        var follow = new UserFollow
        {
            FollowerId = followerId,
            FollowingId = followingId,
            FollowedAt = DateTime.UtcNow
        };

        await context.AddAsync(follow);
        await context.SaveChangesAsync();
    }

    public async Task UnfollowAsync(string followerId, string followingId)
    {
        var follow = await context.UserFollows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

        if (follow == null) return;

        context.Remove(follow);
        await context.SaveChangesAsync();
    }



    public async Task<List<User>> GetFollowersAsync(string userId, int? pageNumber, int? pageSize)
    {
        return await context.UserFollows
             .Where(f => f.FollowingId == userId)
             .Select(f => f.Follower)
             .ApplyPaging(pageNumber, pageSize)
             .ToListAsync();
    }

    public async Task<List<User>> GetFollowingAsync(string userId, int? pageNumber, int? pageSize)
    {
        return await context.UserFollows
         .Where(f => f.FollowerId == userId)
         .Select(f => f.Following)
         .ApplyPaging(pageNumber, pageSize)
         .ToListAsync();
    }

    public async Task<int> GetFollowersCountAsync(string userId) => await context.UserFollows.CountAsync(f => f.FollowingId == userId);
    public async Task<int> GetFollowingCountAsync(string userId) => await context.UserFollows.CountAsync(f => f.FollowerId == userId);

}
