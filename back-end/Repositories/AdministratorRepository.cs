using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            // 对于一对一关系，同样建议使用 Include 预加载关联的 User 数据
            return await _context.Administrators
                                 .Include(a => a.User)
                                 .ToListAsync();
        }

        public async Task<Administrator?> GetByIdAsync(int id)
        {
            // FindAsync 对任何类型的主键都有效
            return await _context.Administrators.FindAsync(id);
        }

        public async Task AddAsync(Administrator administrator)
        {
            await _context.Administrators.AddAsync(administrator);
        }

        public async Task UpdateAsync(Administrator administrator)
        {
            _context.Administrators.Update(administrator);
            await SaveAsync();
        }

        public Task DeleteAsync(Administrator administrator)
        {
            _context.Administrators.Remove(administrator);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}