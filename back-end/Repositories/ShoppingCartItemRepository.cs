using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Set<ShoppingCartItem>().ToListAsync();
        }

        public async Task<ShoppingCartItem?> GetByIdAsync(int id)
        {
            return await _context.Set<ShoppingCartItem>().FindAsync(id);
        }

        public async Task AddAsync(ShoppingCartItem shoppingcartitem)
        {
            _context.Set<ShoppingCartItem>().Add(shoppingcartitem);
            await SaveAsync();
        }

        public async Task UpdateAsync(ShoppingCartItem shoppingcartitem)
        {
            _context.Set<ShoppingCartItem>().Update(shoppingcartitem);
            await SaveAsync();
        }

        public async Task DeleteAsync(ShoppingCartItem shoppingcartitem)
        {
            _context.Set<ShoppingCartItem>().Remove(shoppingcartitem);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}