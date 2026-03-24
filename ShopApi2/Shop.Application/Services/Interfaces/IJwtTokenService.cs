using Shop.Domain.Models;


namespace Shop.Application.Services.Interfaces
{
    public interface IJwtTokenService
    {

        Task<(string AccessToken, DateTimeOffset ExpiresAt)> GenerateAccessTokenAsync(string userId, string email, IList<string> roles);
        Task<(RefreshToken Entity, string Jwt)> CreateRefreshTokenAsync(string userId);
        (string UserId, string Jti) ValidateRefreshTokenAndGetJti(string refreshJwt, bool validateLifetime = true);
        string GetJtiFromRefreshToken(string refreshJwt);


    }
}
