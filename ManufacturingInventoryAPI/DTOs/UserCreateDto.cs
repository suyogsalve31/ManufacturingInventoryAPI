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
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";


    }
}
