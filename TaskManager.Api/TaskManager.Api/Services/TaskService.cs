using System.Text.Json;
using TaskManager.Api.Enums;
using TaskManager.Api.DTOs;
using TaskManager.Api.Entities;
using TaskManager.Api.Repositories;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManagement.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(
            ITaskRepository taskRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
        {
            if (!await _userRepository.ExistsAsync(dto.UserId))
                throw new KeyNotFoundException("El usuario asignado no existe.");

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                Status = TaskStatus.Pending,
                AdditionalData = dto.AdditionalData == null
                    ? null
                    : JsonSerializer.Serialize(dto.AdditionalData)
            };

            var created = await _taskRepository.CreateAsync(task);

            return MapToDto(created);
        }

        public async Task<(IEnumerable<TaskDto> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? userId,
            TaskStatus? status)
        {
            var (items, total) =
                await _taskRepository.GetPagedAsync(page, pageSize, userId, status);

            return (
                items.Select(MapToDto),
                total
            );
        }

         public async Task ChangeStatusAsync(int taskId, TaskStatus newStatus)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new KeyNotFoundException("Task not found");

            // no permitir Pending -> Done
            if (task.Status == TaskStatus.Pending && newStatus == TaskStatus.Done)
            {
                throw new InvalidOperationException(
                    "No se permite cambiar una tarea directamente de Pending a Done."
                );
            }

            task.Status = newStatus;

            await _taskRepository.UpdateAsync(task);
        }

        private static TaskDto MapToDto(TaskItem task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                UserId = task.UserId,
                CreatedAt = task.CreatedAt,
                AdditionalData = string.IsNullOrEmpty(task.AdditionalData)
                    ? null
                    : JsonSerializer.Deserialize<TaskAdditionalDataDto>(task.AdditionalData)
            };
        }
    }
}
