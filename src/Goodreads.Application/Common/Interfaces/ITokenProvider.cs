namespace Goodreads.Application.Common.Interfaces;
public interface ITokenProvider
{
    Task<string> GenerateAccessTokenAsync(User user);
    Task<string> GenerateAndStoreRefreshTokenAsync(User user, string jwtId);
}
