using System.ComponentModel.DataAnnotations;

namespace ManufacturingInventoryAPI.DTOs
{
    public class ProductQueryDto
    {
        public string? SearchTerm { get; set; }
        public bool? IsActive { get; set; }
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        [Range(1, 100)] 
        public int PageSize { get; set; } = 10;
    }
}
