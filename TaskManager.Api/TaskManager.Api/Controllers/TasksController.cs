using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Enums;
using TaskManagement.Api.Services;
using TaskManager.Api.DTOs;
using TaskStatus = TaskManager.Api.Enums.TaskStatus;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = await _taskService.CreateAsync(dto);
            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? userId = null,
            [FromQuery] TaskStatus? status = null)
        {
            var result = await _taskService
                .GetPagedAsync(page, pageSize, userId, status);

            return Ok(new
            {
                items = result.Items,
                total = result.TotalCount,
                page,
                pageSize
            });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(
            int id,
            UpdateTaskStatusDto dto)
        {
            await _taskService.ChangeStatusAsync(id, dto.NewStatus);
            return NoContent();
        }
    }
}
