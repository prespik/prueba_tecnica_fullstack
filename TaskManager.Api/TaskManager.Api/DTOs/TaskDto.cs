using TaskManager.Api.Enums;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManager.Api.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public TaskAdditionalDataDto? AdditionalData { get; set; }
    }
}
