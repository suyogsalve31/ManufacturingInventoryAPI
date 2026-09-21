using ManufacturigInventoryAPI.Models;
using ManufacturigInventoryAPI.Services;
using ManufacturingInventoryAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {

            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create(ProductCreateDto productDto)
        {
            var createdProduct = await _productService.CreateAsync(productDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.ProductId },
                createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDto>> Update(int id, ProductUpdateDto productDto)
        {
            var updatedProduct = await _productService.UpdateAsync(id, productDto);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
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