using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class FoodOrderRepository : IFoodOrderRepository
    {
        private readonly AppDbContext _context;
        public FoodOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodOrder>> GetAllAsync()
        {
            return await _context.Set<FoodOrder>().ToListAsync();
        }

        public async Task<FoodOrder?> GetByIdAsync(int id)
        {
            return await _context.Set<FoodOrder>().FindAsync(id);
        }

        public async Task AddAsync(FoodOrder foodorder)
        {
            _context.Set<FoodOrder>().Add(foodorder);
            await SaveAsync();
        }

        public async Task UpdateAsync(FoodOrder foodorder)
        {
            _context.Set<FoodOrder>().Update(foodorder);
            await SaveAsync();
        }

        public async Task DeleteAsync(FoodOrder foodorder)
        {
            _context.Set<FoodOrder>().Remove(foodorder);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}