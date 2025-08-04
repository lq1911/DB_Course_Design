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
            return await _context.Set<Menu_Dish>().ToListAsync();
        }

        public async Task<Menu_Dish?> GetByIdAsync(int id)
        {
            return await _context.Set<Menu_Dish>().FindAsync(id);
        }

        public async Task AddAsync(Menu_Dish menu_dish)
        {
            _context.Set<Menu_Dish>().Add(menu_dish);
            await SaveAsync();
        }

        public async Task UpdateAsync(Menu_Dish menu_dish)
        {
            _context.Set<Menu_Dish>().Update(menu_dish);
            await SaveAsync();
        }

        public async Task DeleteAsync(Menu_Dish menu_dish)
        {
            _context.Set<Menu_Dish>().Remove(menu_dish);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}