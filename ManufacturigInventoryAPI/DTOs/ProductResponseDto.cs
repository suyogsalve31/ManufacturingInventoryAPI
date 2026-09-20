namespace ManufacturingInventoryAPI.DTOs
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Unit { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}