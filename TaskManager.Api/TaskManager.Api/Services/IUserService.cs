using TaskManager.Api.DTOs;

namespace TaskManager.Api.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
