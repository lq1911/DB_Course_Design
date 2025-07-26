using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Dishes.ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task AddAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
        }

        public async Task UpdateAsync(Dish dish)
        {
            _context.Dishes.Update(dish);
            await SaveAsync();
        }

        public Task DeleteAsync(Dish dish)
        {
            _context.Dishes.Remove(dish);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}