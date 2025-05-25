using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Goodreads.Infrastructure.Repositories;
internal class RefreshTokenRepository(ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken token)
    {
        await dbContext.RefreshTokens.AddAsync(token);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await dbContext.RefreshTokens
             .Include(r => r.User)
             .FirstOrDefaultAsync(r => r.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId)
    {
        var tokens = await dbContext.RefreshTokens.Where(r => r.UserId == userId).ToListAsync();
        return tokens;
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

}
