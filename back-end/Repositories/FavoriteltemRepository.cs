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
            // 预加载关联的 Store 和 Folder 数据
            return await _context.Favorite_Items
                                 .Include(fi => fi.Store)
                                 .Include(fi => fi.Folder)
                                 .ToListAsync();
        }

        public async Task<FavoriteItem?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.Favorite_Items
                                 .Include(fi => fi.Store)
                                 .Include(fi => fi.Folder)
                                 .FirstOrDefaultAsync(fi => fi.ItemID == id);
        }

        public async Task AddAsync(FavoriteItem item)
        {
            await _context.Favorite_Items.AddAsync(item);
        }

        public async Task UpdateAsync(FavoriteItem item)
        {
            _context.Favorite_Items.Update(item);
            await SaveAsync();
        }

        public Task DeleteAsync(FavoriteItem item)
        {
            _context.Favorite_Items.Remove(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}