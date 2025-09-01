using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class FavoritesFolderRepository : IFavoritesFolderRepository
    {
        private readonly AppDbContext _context;
        public FavoritesFolderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoritesFolder>> GetAllAsync()
        {
            return await _context.FavoritesFolders
                                 .Include(ff => ff.Customer)      // 关联顾客
                                 .Include(ff => ff.FavoriteItems) // 收藏夹中的项目
                                 .ToListAsync();
        }

        public async Task<FavoritesFolder?> GetByIdAsync(int id)
        {
            return await _context.FavoritesFolders
                                 .Include(ff => ff.Customer)
                                 .Include(ff => ff.FavoriteItems)
                                 .FirstOrDefaultAsync(ff => ff.FolderID == id);
        }

        public async Task AddAsync(FavoritesFolder favoritesfolder)
        {
            _context.FavoritesFolders.Add(favoritesfolder);
            await SaveAsync();
        }

        public async Task UpdateAsync(FavoritesFolder favoritesfolder)
        {
            _context.FavoritesFolders.Update(favoritesfolder);
            await SaveAsync();
        }

        public async Task DeleteAsync(FavoritesFolder favoritesfolder)
        {
            _context.FavoritesFolders.Remove(favoritesfolder);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}