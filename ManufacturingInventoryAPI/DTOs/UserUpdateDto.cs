using System.ComponentModel.DataAnnotations;

namespace ManufacturingInventoryAPI.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [RegularExpression(
            @"^(?i:Admin|User)$",
            ErrorMessage = "Role must be either Admin or User.")]
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; }
    }
}