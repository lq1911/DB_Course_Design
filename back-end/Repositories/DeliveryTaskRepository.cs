using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class DeliveryTaskRepository : IDeliveryTaskRepository
    {
        private readonly AppDbContext _context;

        public DeliveryTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeliveryTask>> GetAllAsync()
        {
            // 预加载关联的 Customer 和 Store 数据
            return await _context.Delivery_Tasks
                                 .Include(dt => dt.Customer)
                                 .Include(dt => dt.Store)
                                 .ToListAsync();
        }

        public async Task<DeliveryTask?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.Delivery_Tasks
                                 .Include(dt => dt.Customer)
                                 .Include(dt => dt.Store)
                                 .FirstOrDefaultAsync(dt => dt.TaskID == id);
        }

        public async Task AddAsync(DeliveryTask task)
        {
            await _context.Delivery_Tasks.AddAsync(task);
        }

        public async Task UpdateAsync(DeliveryTask task)
        {
            _context.Delivery_Tasks.Update(task);
            await SaveAsync();
        }

        public Task DeleteAsync(DeliveryTask task)
        {
            _context.Delivery_Tasks.Remove(task);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}