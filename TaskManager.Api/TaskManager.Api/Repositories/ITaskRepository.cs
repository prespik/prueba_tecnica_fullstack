using TaskManager.Api.Entities;
using TaskManager.Api.Enums;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManager.Api.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(int id);

        Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? userId,
            TaskStatus? status
        );

        Task UpdateAsync(TaskItem task);
    }
}
