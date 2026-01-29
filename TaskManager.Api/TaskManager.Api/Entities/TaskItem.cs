using System.ComponentModel.DataAnnotations;
using TaskManager.Api.Enums;
using TaskManager.Api.Entities;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManager.Api.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        [Required]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? AdditionalData { get; set; }
    }
}
