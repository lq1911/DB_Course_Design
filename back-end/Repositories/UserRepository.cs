using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; // 确保 using 了这个
using System.Threading.Tasks;   // 确保 using 了这个

namespace BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        // 通过构造函数注入数据库上下文
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // 对数据进行操作
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // --- 这是第一个核心修正 ---
        // 方法签名中的参数类型从 long 修改为 string，以匹配 User 模型
        public async Task<User?> GetByPhoneAsync(string phoneNumber)
        {
            // 现在可以直接进行字符串比较
            return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await SaveAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _context.Set<User>().Update(user);
            await SaveAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // --- 这是第二个核心修正 ---
        // 方法签名中的参数类型已为 string，无需转换
        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            // 直接使用传入的 string 进行比较
            return await _context.Users.AnyAsync(u => u.PhoneNumber == phone);
        }
    }
}