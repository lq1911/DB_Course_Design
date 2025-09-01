using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Couriers
                                 .Include(c => c.User)
                                 .Include(c => c.DeliveryTasks)
                                 .ToListAsync();
        }

        public async Task<Courier?> GetByIdAsync(int id)
        {
            return await _context.Couriers
                                 .Include(c => c.User)
                                 .Include(c => c.DeliveryTasks)
                                 .FirstOrDefaultAsync(c => c.UserID == id);
        }

        public async Task AddAsync(Courier courier)
        {
            await _context.Couriers.AddAsync(courier);
            await SaveAsync();
        }

        public async Task UpdateAsync(Courier courier)
        {
            _context.Couriers.Update(courier);
            await SaveAsync();
        }

        public async Task DeleteAsync(Courier courier)
        {
            _context.Couriers.Remove(courier);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}