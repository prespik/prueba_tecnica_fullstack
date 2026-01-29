using TaskManager.Api.Enums;
using TaskManager.Api.DTOs;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManagement.Api.Services
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskDto dto);

        Task<(IEnumerable<TaskDto> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? userId,
            TaskStatus? status
        );

        Task ChangeStatusAsync(int taskId, TaskStatus newStatus);
    }
}
