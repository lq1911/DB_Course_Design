using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // 获取所有用户
        Task<IEnumerable<User>> GetAllAsync();
        // 根据用户ID获取用户
        Task<User?> GetByIdAsync(int id);
        // 根据手机号获取用户
        Task<User?> GetByPhoneAsync(long phoneNumber);
        // 根据邮箱获取用户
        Task<User?> GetByEmailAsync(string email);
        // 添加一个新用户
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        // 删除指定ID的用户。
        Task DeleteAsync(User user);
        // 保存操作
        Task SaveAsync();
        // 验证手机号是否重复
        Task<bool> ExistsByPhoneAsync(string phone);
    }
}
