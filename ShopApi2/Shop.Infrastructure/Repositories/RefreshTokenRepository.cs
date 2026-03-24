using Microsoft.EntityFrameworkCore;
using Shop.Application.Services.Interfaces;
using Shop.Data;
using Shop.Domain.Models;


namespace Shop.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ShopDbContext _context;

        public RefreshTokenRepository(ShopDbContext context) => _context = context;

        public async Task<RefreshToken> AddAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken?> GetByJwtIdAsync(string jwtId) =>
            await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.JwtId == jwtId);

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }
    
    }
}
