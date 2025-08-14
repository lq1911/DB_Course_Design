using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Stores
                                 .Include(s => s.Seller)
                                 .Include(s => s.FoodOrders)
                                 .Include(s => s.CouponManagers)
                                 .Include(s => s.Menus)
                                 .Include(s => s.FavoriteItems)
                                 .Include(s => s.StoreViolationPenalties)
                                 .Include(s => s.Comments)
                                 .Include(s => s.DeliveryTasks)
                                 .ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(int id)
        {
            return await _context.Stores
                                 .Include(s => s.Seller)
                                 .Include(s => s.FoodOrders)
                                 .Include(s => s.CouponManagers)
                                 .Include(s => s.Menus)
                                 .Include(s => s.FavoriteItems)
                                 .Include(s => s.StoreViolationPenalties)
                                 .Include(s => s.Comments)
                                 .Include(s => s.DeliveryTasks)
                                 .FirstOrDefaultAsync(s => s.StoreID == id);
        }

        public async Task AddAsync(Store store)
        {
            await _context.Stores.AddAsync(store);
            await SaveAsync();
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Stores.Update(store);
            await SaveAsync();
        }

        public async Task DeleteAsync(Store store)
        {
            _context.Stores.Remove(store);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}