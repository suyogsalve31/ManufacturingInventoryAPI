using ManufacturingInventoryAPI.Models;

namespace ManufacturingInventoryAPI.Repositories
{
    public interface IUserRepository
    {

        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);

    }
}
