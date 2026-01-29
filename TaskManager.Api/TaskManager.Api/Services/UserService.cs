using TaskManager.Api.DTOs;
using TaskManager.Api.Entities;
using TaskManager.Api.Repositories;
using TaskManager.Api.Services;

namespace TaskManagement.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            var created = await _userRepository.CreateAsync(user);

            return new UserDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            });
        }
    }
}
