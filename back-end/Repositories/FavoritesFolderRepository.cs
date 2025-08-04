using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Set<FavoritesFolder>().ToListAsync();
        }

        public async Task<FavoritesFolder?> GetByIdAsync(int id)
        {
            return await _context.Set<FavoritesFolder>().FindAsync(id);
        }

        public async Task AddAsync(FavoritesFolder favoritesfolder)
        {
            _context.Set<FavoritesFolder>().Add(favoritesfolder);
            await SaveAsync();
        }

        public async Task UpdateAsync(FavoritesFolder favoritesfolder)
        {
            _context.Set<FavoritesFolder>().Update(favoritesfolder);
            await SaveAsync();
        }

        public async Task DeleteAsync(FavoritesFolder favoritesfolder)
        {
            _context.Set<FavoritesFolder>().Remove(favoritesfolder);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}