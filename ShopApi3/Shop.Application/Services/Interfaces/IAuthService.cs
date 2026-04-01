
using Shop.DTO.Auth_DTOs;


namespace Shop.Application.Services.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        Task RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);

    }
}
