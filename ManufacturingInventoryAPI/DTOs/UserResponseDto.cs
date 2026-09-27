namespace ManufacturingInventoryAPI.DTOs
{
    public class UserResponseDto
    {

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
