using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Set<Store>().ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(int id)
        {
            return await _context.Set<Store>().FindAsync(id);
        }

        public async Task AddAsync(Store store)
        {
            _context.Set<Store>().Add(store);
            await SaveAsync();
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Set<Store>().Update(store);
            await SaveAsync();
        }

        public async Task DeleteAsync(Store store)
        {
            _context.Set<Store>().Remove(store);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}