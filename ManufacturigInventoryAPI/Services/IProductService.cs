using ManufacturigInventoryAPI.Models;
using ManufacturingInventoryAPI.DTOs;

namespace ManufacturigInventoryAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();

        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateAsync(ProductCreateDto productDto);
        Task<ProductResponseDto?> UpdateAsync(int id, ProductUpdateDto productDto);
        Task<bool> DeleteAsync(int id);
    }
}
