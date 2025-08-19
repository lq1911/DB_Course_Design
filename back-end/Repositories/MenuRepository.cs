using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;
        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Menus
                                 .Include(m => m.Store)                  // 关联商店
                                 .Include(m => m.MenuDishes)             // 中间表
                                     .ThenInclude(md => md.Dish)         // 菜品
                                 .ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus
                                 .Include(m => m.Store)
                                 .Include(m => m.MenuDishes)
                                     .ThenInclude(md => md.Dish)
                                 .FirstOrDefaultAsync(m => m.MenuID == id);
        }

        public async Task AddAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await SaveAsync();
        }

        public async Task UpdateAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await SaveAsync();
        }

        public async Task DeleteAsync(Menu menu)
        {
            _context.Menus.Remove(menu);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}