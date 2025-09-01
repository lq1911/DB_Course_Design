using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly AppDbContext _context;

        public DishRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            return await _context.Dishes
                                 .Include(d => d.ShoppingCartItems) // 一对多：购物车项
                                 .Include(d => d.MenuDishes)        // 多对多中间表
                                     .ThenInclude(md => md.Menu)    // 另一端：菜单
                                 .ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes
                                 .Include(d => d.ShoppingCartItems)
                                 .Include(d => d.MenuDishes)
                                     .ThenInclude(md => md.Menu)
                                 .FirstOrDefaultAsync(d => d.DishID == id);
        }

        public async Task AddAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await SaveAsync();
        }

        public async Task UpdateAsync(Dish dish)
        {
            _context.Dishes.Update(dish);
            await SaveAsync();
        }

        public async Task DeleteAsync(Dish dish)
        {
            _context.Dishes.Remove(dish);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}