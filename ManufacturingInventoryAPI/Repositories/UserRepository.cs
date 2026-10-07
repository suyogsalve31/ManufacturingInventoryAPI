using ManufacturingInventoryAPI.Data;
using ManufacturingInventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingInventoryAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<User> Users, int TotalCount)> GetAllAsync(
                                                                        string? searchTerm,
                                                                        bool? isActive,
                                                                        int pageNumber,
                                                                        int pageSize)
        {
            var query = _context.Users
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u =>
                    u.UserName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(u =>
                    u.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.UserId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateAsync(int id, User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.UserName = user.UserName;
            existingUser.Role = user.Role;
            existingUser.IsActive = user.IsActive;

            await _context.SaveChangesAsync();

            return existingUser;
        }


        public async Task<bool> DeactivateAsync(int id) 
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return false;
            }

            user.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}