using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Accept_TaskRepository : IAccept_TaskRepository
    {
        private readonly AppDbContext _context;

        public Accept_TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accept_Task>> GetAllAsync()
        {
            // 【最佳实践】使用 Include 可以在一次数据库查询中加载关联的 Courier 和 DeliveryTask 数据，
            // 避免了后续访问这些属性时产生额外的数据库查询（N+1 问题）。
            // 这不违反示例的核心逻辑，但能极大提升性能。
            return await _context.Accept_Tasks
                                 .Include(at => at.Courier)
                                 .Include(at => at.DeliveryTask)
                                 .ToListAsync();
        }

        public async Task<Accept_Task?> GetByIdAsync(int courierId, int deliveryTaskId)
        {
            // 对于复合主键，FindAsync 是最高效的查询方式。
            return await _context.Accept_Tasks.FindAsync(courierId, deliveryTaskId);
        }

        public async Task AddAsync(Accept_Task acceptTask)
        {
            await _context.Accept_Tasks.AddAsync(acceptTask);
        }

        public async Task UpdateAsync(Accept_Task acceptTask)
        {
            _context.Accept_Tasks.Update(acceptTask);
            await SaveAsync(); // 遵循示例风格，在Update内部直接调用Save
        }

        public Task DeleteAsync(Accept_Task acceptTask)
        {
            _context.Accept_Tasks.Remove(acceptTask);
            // 遵循示例风格，此方法只标记状态，但为了匹配接口定义返回一个已完成的 Task
            return Task.CompletedTask; 
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}