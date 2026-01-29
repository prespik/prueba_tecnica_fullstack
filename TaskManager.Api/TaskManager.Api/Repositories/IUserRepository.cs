using TaskManager.Api.Entities;

namespace TaskManager.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
