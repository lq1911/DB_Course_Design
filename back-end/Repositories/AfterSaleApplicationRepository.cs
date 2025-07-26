using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class AfterSaleApplicationRepository : IAfterSaleApplicationRepository
    {
        private readonly AppDbContext _context;

        public AfterSaleApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AfterSaleApplication>> GetAllAsync()
        {
            // 预加载关联的 Customer 和 Order 数据
            return await _context.After_Sale_Applications
                                 .Include(a => a.Customer)
                                 .Include(a => a.Order) // 这里的 Order 是 AfterSaleApplication 实体中的导航属性名
                                 .ToListAsync();
        }

        public async Task<AfterSaleApplication?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.After_Sale_Applications
                                 .Include(a => a.Customer)
                                 .Include(a => a.Order)
                                 .FirstOrDefaultAsync(a => a.ApplicationID == id);
        }

        public async Task AddAsync(AfterSaleApplication application)
        {
            await _context.After_Sale_Applications.AddAsync(application);
        }

        public async Task UpdateAsync(AfterSaleApplication application)
        {
            _context.After_Sale_Applications.Update(application);
            await SaveAsync();
        }

        public Task DeleteAsync(AfterSaleApplication application)
        {
            _context.After_Sale_Applications.Remove(application);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}