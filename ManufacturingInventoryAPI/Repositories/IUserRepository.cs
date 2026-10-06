using ManufacturingInventoryAPI.Models;

namespace ManufacturingInventoryAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(bool? isActive);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(int id, User user);
        Task<bool> DeactivateAsync(int id);
    }
}
