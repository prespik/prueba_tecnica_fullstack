using System.ComponentModel.DataAnnotations;
using TaskManager.Api.Enums;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManager.Api.DTOs
{
    public class UpdateTaskStatusDto
    {
        [Required]
        public TaskStatus NewStatus { get; set; }
    }
}
