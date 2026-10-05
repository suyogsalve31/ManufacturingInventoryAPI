using ManufacturingInventoryAPI.DTOs;

namespace ManufacturingInventoryAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();

        Task<UserResponseDto?> GetByUsernameAsync(string username);

        Task<UserResponseDto> CreateAsync(UserCreateDto userDto);

        Task<UserResponseDto?> ValidateCredentialsAsync(
                string username,
                string password);

        Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto userDto);

        Task<bool> DeactivateAsync(int id);
    }
}