using ManufacturingInventoryAPI.Services;
using ManufacturingInventoryAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ManufacturigInventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProductResponseDto>>> GetAll([FromQuery] ProductQueryDto query)
        {
            var result = await _productService.GetAllAsync(query);

            return Ok(new ApiResponseDto<PagedResultDto<ProductResponseDto>>
            {
                Success = true,
                Message = "Products fetched successfully.",
                Data = result
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponseDto<ProductResponseDto>
            {
                Success = true,
                Message = "Product fetched successfully.",
                Data = product
            });
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create(ProductCreateDto productDto)
        {
            var createdProduct = await _productService.CreateAsync(productDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.ProductId },
                new ApiResponseDto<ProductResponseDto>
            {
                Success = true,
                Message = "Product created successfully.",
                Data = createdProduct
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDto>> Update(int id, ProductUpdateDto productDto)
        {
            var updatedProduct = await _productService.UpdateAsync(id, productDto);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponseDto<ProductResponseDto>
            {
                Success = true,
                Message = "Product updated successfully.",
                Data = updatedProduct
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}