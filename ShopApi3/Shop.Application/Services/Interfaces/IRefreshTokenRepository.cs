using Shop.Domain.Models;


namespace Shop.Application.Services.Interfaces
{
    public interface IRefreshTokenRepository
    {

        Task<RefreshToken?> GetByJwtIdAsync(string jwtId);
        Task<RefreshToken> AddAsync(RefreshToken refreshToken);
        Task UpdateAsync(RefreshToken refreshToken);

    }
}
