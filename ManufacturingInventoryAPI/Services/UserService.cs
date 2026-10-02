using ManufacturingInventoryAPI.DTOs;
using ManufacturingInventoryAPI.Middleware;
using ManufacturingInventoryAPI.Models;
using ManufacturingInventoryAPI.Repositories;

namespace ManufacturingInventoryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            });
        }


        public async Task<UserResponseDto?> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
            {
                return null;
            }

            return new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            };
        }


        public async Task<UserResponseDto?> ValidateCredentialsAsync(
                                                    string username,
                                                    string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
            {
                return null;
            }

            if (!user.IsActive)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            };
        }


        public async Task<UserResponseDto> CreateAsync(UserCreateDto userDto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(userDto.UserName);

            if (existingUser != null)       
            {
                throw new BusinessException("Username already exists.");
            }

            if (!string.Equals(userDto.Role, "Admin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(userDto.Role, "User", StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException("Invalid role. Allowed roles are Admin or User.");
            }

            var user = new User
            {
                UserName = userDto.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = userDto.Role,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);

            return new UserResponseDto
            {
                UserId = createdUser.UserId,
                UserName = createdUser.UserName,
                Role = createdUser.Role,
                IsActive = createdUser.IsActive,
                CreatedDate = createdUser.CreatedDate
            };
        }
    }
}