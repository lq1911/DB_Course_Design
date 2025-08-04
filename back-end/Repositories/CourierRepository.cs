using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class CourierRepository : ICourierRepository
    {
        private readonly AppDbContext _context;

        public CourierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Courier>> GetAllAsync()
        {
            // 预加载关联的 User 数据
            return await _context.Couriers
                                 .Include(c => c.User)
                                 .ToListAsync();
        }

        public async Task<Courier?> GetByIdAsync(int id)
        {
            return await _context.Couriers.FindAsync(id);
        }

        public async Task AddAsync(Courier courier)
        {
            await _context.Couriers.AddAsync(courier);
        }

        public async Task UpdateAsync(Courier courier)
        {
            _context.Couriers.Update(courier);
            await SaveAsync();
        }

        public Task DeleteAsync(Courier courier)
        {
            _context.Couriers.Remove(courier);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}