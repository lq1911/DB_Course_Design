using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            return await _context.ShoppingCarts
                                 .Include(sc => sc.ShoppingCartItems) // 购物车项
                                 .ToListAsync();
        }

        public async Task<ShoppingCart?> GetByIdAsync(int id)
        {
            return await _context.ShoppingCarts
                                 .Include(sc => sc.Order)
                                 .Include(sc => sc.ShoppingCartItems)
                                     .ThenInclude(sci => sci.Dish)
                                 .Include(sc => sc.Customer)
                                 .Include(sc => sc.Store)
                                 .FirstOrDefaultAsync(sc => sc.CartID == id);
        }

        public async Task<ShoppingCart?> GetActiveCartWithStoreFilterAsync(int customerId, int storeId)
        {
            return await _context.ShoppingCarts
                .AsNoTracking()
                .Include(c => c.ShoppingCartItems!)
                    .ThenInclude(i => i.Dish)
                .Where(c => c.CustomerID == customerId &&
                        c.ShoppingCartState == ShoppingCartState.Active &&
                        c.StoreID == storeId)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await SaveAsync();
        }

        public async Task UpdateAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            await SaveAsync();
        }

        public async Task DeleteAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}