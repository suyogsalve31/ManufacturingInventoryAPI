using ManufacturingInventoryAPI.Models;
using ManufacturingInventoryAPI.Repositories;
using ManufacturingInventoryAPI.DTOs;
using ManufacturingInventoryAPI.Services;
using Microsoft.Extensions.Logging;

namespace ManufacturingInventoryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<PagedResultDto<ProductResponseDto>> GetAllAsync(
    ProductQueryDto query)
        {
            _logger.LogInformation(
                "Fetching products. SearchTerm: {SearchTerm}, IsActive: {IsActive}, PageNumber: {PageNumber}, PageSize: {PageSize}",
                query.SearchTerm,
                query.IsActive,
                query.PageNumber,
                query.PageSize);

            var result = await _productRepository.GetAllAsync(
                query.SearchTerm,
                query.IsActive,
                query.PageNumber,
                query.PageSize);

            var productDtos = result.Products.Select(product => new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                Unit = product.Unit,
                UnitPrice = product.UnitPrice,
                ReorderLevel = product.ReorderLevel,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedDate
            }).ToList();

            var totalPages = (int)Math.Ceiling(
                result.TotalCount / (double)query.PageSize);

            return new PagedResultDto<ProductResponseDto>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = totalPages,
                Data = productDtos
            };
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                Unit = product.Unit,
                UnitPrice = product.UnitPrice,
                ReorderLevel = product.ReorderLevel,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedDate
            };
        }
        public async Task<ProductResponseDto> CreateAsync(ProductCreateDto productDto)
        {
            _logger.LogInformation("Creating product with code: {ProductCode}", productDto.ProductCode);

            var product = new Product
            {
                ProductCode = productDto.ProductCode,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Unit = productDto.Unit,
                UnitPrice = productDto.UnitPrice,
                ReorderLevel = productDto.ReorderLevel,
                IsActive = productDto.IsActive
            };

            var createdProduct = await _productRepository.CreateAsync(product);

            return new ProductResponseDto
            {
                ProductId = createdProduct.ProductId,
                ProductCode = createdProduct.ProductCode,
                ProductName = createdProduct.ProductName,
                Description = createdProduct.Description,
                Unit = createdProduct.Unit,
                UnitPrice = createdProduct.UnitPrice,
                ReorderLevel = createdProduct.ReorderLevel,
                IsActive = createdProduct.IsActive,
                CreatedDate = createdProduct.CreatedDate
            };
        }
        public async Task<ProductResponseDto?> UpdateAsync(int id,ProductUpdateDto productDto)
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);

            var product = new Product
            {
                ProductCode = productDto.ProductCode,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Unit = productDto.Unit,
                UnitPrice = productDto.UnitPrice,
                ReorderLevel = productDto.ReorderLevel,
                IsActive = productDto.IsActive
            };

            var updatedProduct = await _productRepository.UpdateAsync(id, product);

            if (updatedProduct == null)
            {
                return null;
            }

            return new ProductResponseDto
            {
                ProductId = updatedProduct.ProductId,
                ProductCode = updatedProduct.ProductCode,
                ProductName = updatedProduct.ProductName,
                Description = updatedProduct.Description,
                Unit = updatedProduct.Unit,
                UnitPrice = updatedProduct.UnitPrice,
                ReorderLevel = updatedProduct.ReorderLevel,
                IsActive = updatedProduct.IsActive,
                CreatedDate = updatedProduct.CreatedDate
            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("deleting product with ID: {ProductId}", id);

            return await _productRepository.DeleteAsync(id);
        }

    }
}
