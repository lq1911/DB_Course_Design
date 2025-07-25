using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Set<ShoppingCart>().ToListAsync();
        }

        public async Task<ShoppingCart?> GetByIdAsync(int id)
        {
            return await _context.Set<ShoppingCart>().FindAsync(id);
        }

        public async Task AddAsync(ShoppingCart shoppingcart)
        {
            _context.Set<ShoppingCart>().Add(shoppingcart);
            await SaveAsync();
        }

        public async Task UpdateAsync(ShoppingCart shoppingcart)
        {
            _context.Set<ShoppingCart>().Update(shoppingcart);
            await SaveAsync();
        }

        public async Task DeleteAsync(ShoppingCart shoppingcart)
        {
            _context.Set<ShoppingCart>().Remove(shoppingcart);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}