using ManufacturigInventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturigInventoryAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
