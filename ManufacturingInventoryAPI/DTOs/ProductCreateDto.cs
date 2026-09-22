using System.ComponentModel.DataAnnotations;

namespace ManufacturingInventoryAPI.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty;

        [Range(0.01, 999999999)]
        public decimal UnitPrice { get; set; }

        [Range(0, 999999)]
        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; } = true;
    }
}