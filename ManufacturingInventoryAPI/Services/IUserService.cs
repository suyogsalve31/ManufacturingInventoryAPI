using ManufacturingInventoryAPI.Models;

namespace ManufacturingInventoryAPI.Services
{
    public interface IUserService
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User> CreateAsync(User user);
    }
}