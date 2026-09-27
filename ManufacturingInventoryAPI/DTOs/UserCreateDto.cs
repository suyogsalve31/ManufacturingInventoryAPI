using System.ComponentModel.DataAnnotations;

namespace ManufacturingInventoryAPI.DTOs
{
    public class UserCreateDto
    {

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";


    }
}
