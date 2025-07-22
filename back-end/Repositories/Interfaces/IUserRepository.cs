using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // 获取所有用户
        Task<IEnumerable<User>> GetAllAsync();
        // 根据用户ID获取用户
        Task<User?> GetByIdAsync(int id);
        // 添加一个新用户
        Task AddAsync(User user);
        // 更新指定用户信息
        Task<bool> UpdateUserAsync(User user);
        // 删除指定ID的用户。
        Task DeleteAsync(User user);
        // 保存操作
        Task SaveAsync();
    }
}
