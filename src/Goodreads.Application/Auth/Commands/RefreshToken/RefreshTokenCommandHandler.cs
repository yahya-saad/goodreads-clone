using System.IdentityModel.Tokens.Jwt;

namespace Goodreads.Application.Auth.Commands.RefreshToken;
internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResultDto>>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokenCommandHandler(ITokenProvider tokenProvider, IRefreshTokenRepository refreshTokenRepository, ILogger<RefreshTokenCommandHandler> logger)
    {
        _tokenProvider = tokenProvider;
        _refreshTokenRepository = refreshTokenRepository;
    }
    public async Task<Result<AuthResultDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (storedRefreshToken == null || storedRefreshToken.IsUsed || storedRefreshToken.IsRevoked || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
            return Result<AuthResultDto>.Fail(Error.Failure("Auth.InvalidRefreshToken", "Invalid refresh token"));

        storedRefreshToken.IsUsed = true;
        await _refreshTokenRepository.SaveChangesAsync();

        var accessToken = await _tokenProvider.GenerateAccessTokenAsync(storedRefreshToken.User);
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
        var refreshToken = await _tokenProvider.GenerateAndStoreRefreshTokenAsync(storedRefreshToken.User, jwtToken.Id);

        var authResult = new AuthResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return (Result<AuthResultDto>.Ok(authResult));
    }
}
