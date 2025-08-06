using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class DeliveryComplaintRepository : IDeliveryComplaintRepository
    {
        private readonly AppDbContext _context;

        public DeliveryComplaintRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeliveryComplaint>> GetAllAsync()
        {
            // 预加载所有关联的实体数据
            return await _context.Delivery_Complaints
                                 .Include(dc => dc.Courier)
                                 .Include(dc => dc.Customer)
                                 .Include(dc => dc.DeliveryTask)
                                 .ToListAsync();
        }

        public async Task<DeliveryComplaint?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.Delivery_Complaints
                                 .Include(dc => dc.Courier)
                                 .Include(dc => dc.Customer)
                                 .Include(dc => dc.DeliveryTask)
                                 .FirstOrDefaultAsync(dc => dc.ComplaintID == id);
        }

        public async Task AddAsync(DeliveryComplaint complaint)
        {
            await _context.Delivery_Complaints.AddAsync(complaint);
        }

        public async Task UpdateAsync(DeliveryComplaint complaint)
        {
            _context.Delivery_Complaints.Update(complaint);
            await SaveAsync();
        }

        public Task DeleteAsync(DeliveryComplaint complaint)
        {
            _context.Delivery_Complaints.Remove(complaint);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}