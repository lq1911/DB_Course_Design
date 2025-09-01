using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Menu_DishRepository : IMenu_DishRepository
    {
        private readonly AppDbContext _context;
        public Menu_DishRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu_Dish>> GetAllAsync()
        {
            return await _context.Menu_Dishes
                                 .Include(md => md.Menu)  // 关联菜单
                                 .Include(md => md.Dish)  // 关联菜品
                                 .ToListAsync();
        }

        public async Task<Menu_Dish?> GetByIdAsync(int menuId, int dishId)
        {
            return await _context.Menu_Dishes
                                 .Include(md => md.Menu)
                                 .Include(md => md.Dish)
                                 .FirstOrDefaultAsync(md => md.MenuID == menuId && md.DishID == dishId);
        }

        public async Task AddAsync(Menu_Dish menuDish)
        {
            await _context.Menu_Dishes.AddAsync(menuDish);
            await SaveAsync();
        }

        public async Task UpdateAsync(Menu_Dish menuDish)
        {
            _context.Menu_Dishes.Update(menuDish);
            await SaveAsync();
        }

        public async Task DeleteAsync(Menu_Dish menuDish)
        {
            _context.Menu_Dishes.Remove(menuDish);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}