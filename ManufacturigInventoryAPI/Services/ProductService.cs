using ManufacturigInventoryAPI.Models;
using ManufacturigInventoryAPI.Repositories;
using ManufacturingInventoryAPI.DTOs;

namespace ManufacturigInventoryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(product => new ProductResponseDto
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
            });
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
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
            return await _productRepository.DeleteAsync(id);
        }

    }
}
