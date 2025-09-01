using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.DeliveryComplaints
                                 .Include(dc => dc.Courier)
                                 .Include(dc => dc.Customer)
                                 .Include(dc => dc.DeliveryTask)
                                 .Include(dc => dc.EvaluateComplaints)
                                     .ThenInclude(ec => ec.Admin)
                                 .ToListAsync();
        }

        public async Task<DeliveryComplaint?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.DeliveryComplaints
                                 .Include(dc => dc.Courier)
                                 .Include(dc => dc.Customer)
                                 .Include(dc => dc.DeliveryTask)
                                 .Include(dc => dc.EvaluateComplaints)
                                     .ThenInclude(ec => ec.Admin)
                                 .FirstOrDefaultAsync(dc => dc.ComplaintID == id);
        }

        public async Task AddAsync(DeliveryComplaint complaint)
        {
            await _context.DeliveryComplaints.AddAsync(complaint);
            await SaveAsync();
        }

        public async Task UpdateAsync(DeliveryComplaint complaint)
        {
            _context.DeliveryComplaints.Update(complaint);
            await SaveAsync();
        }

        public async Task DeleteAsync(DeliveryComplaint complaint)
        {
            _context.DeliveryComplaints.Remove(complaint);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}