using ManufacturingInventoryAPI.DTOs;

namespace ManufacturingInventoryAPI.Services
{
    public interface IProductService
    {
        Task<PagedResultDto<ProductResponseDto>> GetAllAsync(ProductQueryDto query);

        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateAsync(ProductCreateDto productDto);
        Task<ProductResponseDto?> UpdateAsync(int id, ProductUpdateDto productDto);
        Task<bool> DeleteAsync(int id);
    }
}
