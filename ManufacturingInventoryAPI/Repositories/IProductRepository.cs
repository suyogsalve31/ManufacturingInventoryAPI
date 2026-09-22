using ManufacturingInventoryAPI.Models;

namespace ManufacturingInventoryAPI.Repositories
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAsync(
            string? searchTerm,
            bool? isActive,
            int pageNumber,
            int pageSize);
        Task<Product?> GetByIdAsync(int id);
        Task<Product>CreateAsync(Product product);
        Task<Product?>UpdateAsync(int id, Product product);
        Task<bool>DeleteAsync(int id);
    }
}
