using AutoMapper;
using inventorybackend.Api.DTOs.User;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace inventorybackend.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with username {username} not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with email {email} not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
        {
            var users = await _userRepository.GetActiveUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createDto)
        {
            if (await _userRepository.ExistsByUsernameAsync(createDto.Username))
            {
                throw new InvalidOperationException($"A user with username {createDto.Username} already exists.");
            }

            if (await _userRepository.ExistsByEmailAsync(createDto.Email))
            {
                throw new InvalidOperationException($"A user with email {createDto.Email} already exists.");
            }

            var user = _mapper.Map<User>(createDto);
            user.PasswordHash = HashPassword(createDto.Password);
            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }

        public async Task<UserDto> UpdateAsync(int id, UpdateUserDto updateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            if (existingUser.Email != updateDto.Email && 
                await _userRepository.ExistsByEmailAsync(updateDto.Email))
            {
                throw new InvalidOperationException($"A user with email {updateDto.Email} already exists.");
            }

            _mapper.Map(updateDto, existingUser);
            
            if (!string.IsNullOrEmpty(updateDto.Password))
            {
                existingUser.PasswordHash = HashPassword(updateDto.Password);
            }

            var updatedUser = await _userRepository.UpdateAsync(existingUser);
            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _userRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await _userRepository.DeleteAsync(id);
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            if (!await _userRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            await _userRepository.UpdateStatusAsync(id, isActive);
        }

        public async Task UpdatePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            if (user.PasswordHash != HashPassword(currentPassword))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            await _userRepository.UpdatePasswordAsync(id, HashPassword(newPassword));
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
