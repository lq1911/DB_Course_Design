using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Evaluate_AfterSales
                                 .Include(eas => eas.Admin)
                                 .Include(eas => eas.Application)
                                 .ToListAsync();
        }

        public async Task<Evaluate_AfterSale?> GetByIdAsync(int adminId, int applicationId)
        {
            return await _context.Evaluate_AfterSales
                                 .Include(eas => eas.Admin)
                                 .Include(eas => eas.Application)
                                 .FirstOrDefaultAsync(eas => eas.AdminID == adminId && eas.ApplicationID == applicationId);
        }

        public async Task AddAsync(Evaluate_AfterSale evaluateAfterSale)
        {
            await _context.Evaluate_AfterSales.AddAsync(evaluateAfterSale);
            await SaveAsync();
        }

        public async Task UpdateAsync(Evaluate_AfterSale evaluateAfterSale)
        {
            _context.Evaluate_AfterSales.Update(evaluateAfterSale);
            await SaveAsync();
        }

        public async Task DeleteAsync(Evaluate_AfterSale evaluateAfterSale)
        {
            _context.Evaluate_AfterSales.Remove(evaluateAfterSale);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}