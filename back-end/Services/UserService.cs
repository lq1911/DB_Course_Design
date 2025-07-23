using BackEnd.Dtos.User;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        // 通过构造函数注入仓储层
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            // 调用仓储层的获取所有用户数据的操作
            var users = await _repo.GetAllAsync();

            // 将实体类映射为 DTO，即获取需要传输至前端的数据
            return users.Select(u => new UserDto
            {
                UserID = u.UserID,
                Username = u.Username,
                Email = u.Email
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            // 调用仓储层的指定用户数据的操作
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                AccountCreationTime = DateTime.UtcNow
            };

            // 在仓储层添加用户的操作
            await _repo.AddAsync(user);
            // 仓储层进行保存的操作
            await _repo.SaveAsync();

            return new UserDto
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            // 修改仓库层数据
            user.Username = dto.Username;
            user.Password = dto.Password;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;

            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            // 删除仓库层指定用户的操作
            await _repo.DeleteAsync(user);
            await _repo.SaveAsync();
            return true;
        }
    }
}
