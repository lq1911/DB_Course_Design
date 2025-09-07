using BackEnd.Data;
using BackEnd.Models;
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
                                 .Include(sc => sc.Order)             // 关联订单
                                 .Include(sc => sc.ShoppingCartItems) // 购物车项
                                 .Include(sc => sc.Customer)          // 购物车的消费者
                                 .ToListAsync();
        }

        public async Task<ShoppingCart?> GetByIdAsync(int id)
        {
            return await _context.ShoppingCarts
                                 .Include(sc => sc.Order)
                                 .Include(sc => sc.ShoppingCartItems)
                                 .Include(sc => sc.Customer)
                                 .FirstOrDefaultAsync(sc => sc.CartID == id);
        }
        public async Task<ShoppingCart?> GetByCustomerIdAsync(int customerId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.ShoppingCartItems)
                    .ThenInclude(sci => sci.Dish)
                        .ThenInclude(d => d.MenuDishes)
                            .ThenInclude(md => md.Menu)
                                .ThenInclude(m => m.Store)
                .FirstOrDefaultAsync(sc => sc.CustomerID == customerId);

            return shoppingCart;
        }

        public async Task<ShoppingCart?> GetActiveCartByCustomerIdAsync(int customerId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.ShoppingCartItems)
                    .ThenInclude(i => i.Dish)
                .Where(c => c.CustomerID == customerId && !c.IsLocked)
                .OrderByDescending(c => c.LastUpdatedTime) // 如果有多个未锁定，取最近的
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