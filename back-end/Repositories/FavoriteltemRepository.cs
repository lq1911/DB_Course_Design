using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.FavoriteItems
                                 .Include(fi => fi.Store)
                                 .Include(fi => fi.Folder)
                                 .ToListAsync();
        }

        public async Task<FavoriteItem?> GetByIdAsync(int id)
        {
            return await _context.FavoriteItems
                                 .Include(fi => fi.Store)
                                 .Include(fi => fi.Folder)
                                 .FirstOrDefaultAsync(fi => fi.ItemID == id);
        }

        public async Task AddAsync(FavoriteItem item)
        {
            await _context.FavoriteItems.AddAsync(item);
            await SaveAsync();
        }

        public async Task UpdateAsync(FavoriteItem item)
        {
            _context.FavoriteItems.Update(item);
            await SaveAsync();
        }

        public async Task DeleteAsync(FavoriteItem item)
        {
            _context.FavoriteItems.Remove(item);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}