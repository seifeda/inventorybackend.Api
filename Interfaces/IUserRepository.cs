using inventorybackend.Api.Models;

namespace inventorybackend.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
        Task UpdateStatusAsync(int id, bool isActive);
        Task UpdatePasswordAsync(int id, string hashedPassword);
    }
} 