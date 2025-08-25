using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllAsync()
        {
            return await _context.ShoppingCartItems
                                 .Include(sci => sci.Cart) // 关联购物车
                                 .Include(sci => sci.Dish) // 关联菜品
                                 .ToListAsync();
        }

        public async Task<ShoppingCartItem?> GetByIdAsync(int id)
        {
            return await _context.ShoppingCartItems
                                 .Include(sci => sci.Cart)
                                 .Include(sci => sci.Dish)
                                 .FirstOrDefaultAsync(sci => sci.ItemID == id);
        }

        //新增
        public async Task<IEnumerable<ShoppingCartItem>> GetByCartIdAsync(int cartId)
        {
            return await _context.ShoppingCartItems
                                .Include(sci => sci.Cart)
                                .Include(sci => sci.Dish)
                                .Where(sci => sci.CartID == cartId)
                                .ToListAsync();
        }
        
        public async Task AddAsync(ShoppingCartItem shoppingCartItem)
        {
            await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            await SaveAsync();
        }

        public async Task UpdateAsync(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Update(shoppingCartItem);
            await SaveAsync();
        }

        public async Task DeleteAsync(ShoppingCartItem shoppingCartItem)
        {
            _context.ShoppingCartItems.Remove(shoppingCartItem);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}