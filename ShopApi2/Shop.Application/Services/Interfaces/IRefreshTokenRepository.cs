using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Services.Interfaces
{
    public interface IRefreshTokenRepository
    {

        Task<RefreshToken?> GetByJwtIdAsync(string jwtId);
        Task<RefreshToken> AddAsync(RefreshToken refreshToken);
        Task UpdateAsync(RefreshToken refreshToken);

    }
}
