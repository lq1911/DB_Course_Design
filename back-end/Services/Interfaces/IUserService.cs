using BackEnd.Dtos;

namespace BackEnd.Services.Interfaces
{
    public interface IUserService
    {
        // 获取所有用户
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        // 根据ID获取用户
        Task<UserDto?> GetUserByIdAsync(int id);
        // 创建新用户
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        // 更新用户
        Task<bool> UpdateUserAsync(int id, CreateUserDto dto);
        // 删除用户
        Task<bool> DeleteUserAsync(int id);
    }
}
