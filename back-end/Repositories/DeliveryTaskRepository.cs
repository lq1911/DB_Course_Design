using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.DeliveryTasks
                                 .Include(dt => dt.Customer)
                                 .Include(dt => dt.Store)
                                 .Include(dt => dt.Courier)
                                 .Include(dt => dt.DeliveryComplaints)
                                 .ToListAsync();
        }

        public async Task<DeliveryTask?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.DeliveryTasks
                                 .Include(dt => dt.Customer)
                                 .Include(dt => dt.Store)
                                 .Include(dt => dt.Courier)
                                 .Include(dt => dt.DeliveryComplaints)
                                 .FirstOrDefaultAsync(dt => dt.TaskID == id);
        }

        public async Task AddAsync(DeliveryTask task)
        {
            await _context.DeliveryTasks.AddAsync(task);
            await SaveAsync();
        }

        public async Task UpdateAsync(DeliveryTask task)
        {
            _context.DeliveryTasks.Update(task);
            await SaveAsync();
        }

        public async Task DeleteAsync(DeliveryTask task)
        {
            _context.DeliveryTasks.Remove(task);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<DeliveryTask> GetQueryable()
        {
            return _context.DeliveryTasks.AsQueryable();
        }
    }
}