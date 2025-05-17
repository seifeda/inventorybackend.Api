using inventorybackend.Api.DTOs.Auth;

namespace inventorybackend.Api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> ValidateTokenAsync(string token);
    }
} 