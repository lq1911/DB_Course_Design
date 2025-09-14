using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class StoreViolationPenaltyRepository : IStoreViolationPenaltyRepository
    {
        private readonly AppDbContext _context;
        public StoreViolationPenaltyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoreViolationPenalty>> GetAllAsync()
        {
            return await _context.StoreViolationPenalties
                                 .Include(p => p.Store)       // 关联店铺
                                 .Include(p => p.Supervise_s) // 关联管理员监督关系
                                 .ToListAsync();
        }

        public async Task<StoreViolationPenalty?> GetByIdAsync(int id)
        {
            return await _context.StoreViolationPenalties
                                 .Include(p => p.Store)
                                 .Include(p => p.Supervise_s)
                                 .FirstOrDefaultAsync(p => p.PenaltyID == id);
        }

        public async Task<IEnumerable<StoreViolationPenalty>> GetBySellerIdAsync(int sellerId)
        {
            return await _context.StoreViolationPenalties
                                 .Include(p => p.Store)
                                     .ThenInclude(s => s.Seller)
                                 .Where(p => p.Store.SellerID == sellerId)
                                 .OrderBy(p => p.PenaltyID)
                                 .ToListAsync();
        }

        public async Task AddAsync(StoreViolationPenalty storeViolationPenalty)
        {
            await _context.StoreViolationPenalties.AddAsync(storeViolationPenalty);
            await SaveAsync();
        }

        public async Task UpdateAsync(StoreViolationPenalty storeViolationPenalty)
        {
            _context.StoreViolationPenalties.Update(storeViolationPenalty);
            await SaveAsync();
        }

        public async Task DeleteAsync(StoreViolationPenalty storeViolationPenalty)
        {
            _context.StoreViolationPenalties.Remove(storeViolationPenalty);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}