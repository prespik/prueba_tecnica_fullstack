using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = null!;
    }
}
