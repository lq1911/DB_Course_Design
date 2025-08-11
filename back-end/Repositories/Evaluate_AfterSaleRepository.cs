using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Evaluate_AfterSaleRepository : IEvaluate_AfterSaleRepository
    {
        private readonly AppDbContext _context;

        public Evaluate_AfterSaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evaluate_AfterSale>> GetAllAsync()
        {
            // 预加载关联的 Admin 和 Application 数据
            return await _context.Evaluate_After_Sale
                                 .Include(eas => eas.Admin)
                                 .Include(eas => eas.Application)
                                 .ToListAsync();
        }

        public async Task<Evaluate_AfterSale?> GetByIdAsync(int adminId, int applicationId)
        {
            // 对于复合主键，使用 FindAsync 并按顺序传入主键值
            return await _context.Evaluate_After_Sale.FindAsync(adminId, applicationId);
        }

        public async Task AddAsync(Evaluate_AfterSale evaluateAfterSale)
        {
            await _context.Evaluate_After_Sale.AddAsync(evaluateAfterSale);
        }

        public async Task UpdateAsync(Evaluate_AfterSale evaluateAfterSale)
        {
            _context.Evaluate_After_Sale.Update(evaluateAfterSale);
            await SaveAsync();
        }

        public Task DeleteAsync(Evaluate_AfterSale evaluateAfterSale)
        {
            _context.Evaluate_After_Sale.Remove(evaluateAfterSale);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}