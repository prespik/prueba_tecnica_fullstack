using System.ComponentModel.DataAnnotations;
using TaskManager.Api.Enums;

namespace TaskManager.Api.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int UserId { get; set; }

        public TaskAdditionalDataDto? AdditionalData { get; set; }
    }
}
