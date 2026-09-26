using ManufacturingInventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingInventoryAPI.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }

            var adminUser = new User
            {
                UserName = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                IsActive = true
            };

            context.Users.Add(adminUser);

            await context.SaveChangesAsync();
        }
    }
}