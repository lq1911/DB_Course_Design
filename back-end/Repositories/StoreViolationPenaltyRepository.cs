using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Set<StoreViolationPenalty>().ToListAsync();
        }

        public async Task<StoreViolationPenalty?> GetByIdAsync(int id)
        {
            return await _context.Set<StoreViolationPenalty>().FindAsync(id);
        }

        public async Task AddAsync(StoreViolationPenalty storeviolationpenalty)
        {
            _context.Set<StoreViolationPenalty>().Add(storeviolationpenalty);
            await SaveAsync();
        }

        public async Task UpdateAsync(StoreViolationPenalty storeviolationpenalty)
        {
            _context.Set<StoreViolationPenalty>().Update(storeviolationpenalty);
            await SaveAsync();
        }

        public async Task DeleteAsync(StoreViolationPenalty storeviolationpenalty)
        {
            _context.Set<StoreViolationPenalty>().Remove(storeviolationpenalty);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}