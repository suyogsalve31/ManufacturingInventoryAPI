using ManufacturingInventoryAPI.Data;
using ManufacturingInventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingInventoryAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllAsync(
            string? searchTerm,
            bool? isActive,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Products
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.ProductCode.Contains(searchTerm) ||
                    p.ProductName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.ProductCode = product.ProductCode;
            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Unit = product.Unit;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.ReorderLevel = product.ReorderLevel;
            existingProduct.IsActive = product.IsActive;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}