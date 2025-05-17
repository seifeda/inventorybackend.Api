using inventorybackend.Api.DTOs.User;

namespace inventorybackend.Api.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetActiveUsersAsync();
        Task<UserDto> CreateAsync(CreateUserDto createDto);
        Task<UserDto> UpdateAsync(int id, UpdateUserDto updateDto);
        Task DeleteAsync(int id);
        Task UpdateStatusAsync(int id, bool isActive);
        Task UpdatePasswordAsync(int id, string currentPassword, string newPassword);
    }
} 