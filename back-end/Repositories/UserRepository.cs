using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

		public async Task AddAsync(User user)
		{
		    await _context.Users.AddAsync(user);
		}

		public async Task DeleteAsync(User user)
		{
		    _context.Users.Remove(user);
		}

		public async Task SaveAsync()
		{
		    await _context.SaveChangesAsync();
		}
	}
}
