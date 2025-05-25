namespace Goodreads.Application.Common.Interfaces;
public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId);
    Task SaveChangesAsync();
}
