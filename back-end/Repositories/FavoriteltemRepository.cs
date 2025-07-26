using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class FavoriteItemRepository : IFavoriteItemRepository
    {
        private readonly AppDbContext _context;

        public FavoriteItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoriteItem>> GetAllAsync()
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "FavoriteItems"
            return await _context.FavoriteItems
                                 .Include(fi => fi.Store)
                                 .Include(fi => fi.Folder)
                                 .ToListAsync();
        }

        public async Task<FavoriteItem?> GetByIdAsync(int id)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "FavoriteItems"
            return await _context.FavoriteItems
                                 .Include(fi => fi.Store)
                                 .Include(fi => fi.Folder)
                                 .FirstOrDefaultAsync(fi => fi.ItemID == id);
        }

        public async Task AddAsync(FavoriteItem item)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "FavoriteItems"
            await _context.FavoriteItems.AddAsync(item);
        }

        public async Task UpdateAsync(FavoriteItem item)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "FavoriteItems"
            _context.FavoriteItems.Update(item);
            await SaveAsync();
        }

        public Task DeleteAsync(FavoriteItem item)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "FavoriteItems"
            _context.FavoriteItems.Remove(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}