using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entities;
using TaskManager.Api.Repositories;

namespace TaskManager.Api.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? userId,
            Enums.TaskStatus? status)
        {
            var query = _context.Tasks
                .Include(t => t.User)
                .AsQueryable();

            if (userId.HasValue)
                query = query.Where(t => t.UserId == userId.Value);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
