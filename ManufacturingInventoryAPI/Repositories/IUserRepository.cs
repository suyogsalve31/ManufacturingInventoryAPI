using ManufacturingInventoryAPI.Models;

namespace ManufacturingInventoryAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(int id, User user);

    }
}
