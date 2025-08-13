using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class Evaluate_ComplaintRepository : IEvaluate_ComplaintRepository
    {
        private readonly AppDbContext _context;

        public Evaluate_ComplaintRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evaluate_Complaint>> GetAllAsync()
        {
            // 预加载关联的 Admin 和 Complaint 数据
            return await _context.Evaluate_Complaints
                                 .Include(ec => ec.Admin)
                                 .Include(ec => ec.Complaint)
                                 .ToListAsync();
        }

        public async Task<Evaluate_Complaint?> GetByIdAsync(int adminId, int complaintId)
        {
            return await _context.Evaluate_Complaints
                                 .Include(ec => ec.Admin)
                                 .Include(ec => ec.Complaint)
                                 .FirstOrDefaultAsync(ec => ec.AdminID == adminId && ec.ComplaintID == complaintId);
        }

        public async Task AddAsync(Evaluate_Complaint evaluateComplaint)
        {
            await _context.Evaluate_Complaints.AddAsync(evaluateComplaint);
            await SaveAsync();
        }

        public async Task UpdateAsync(Evaluate_Complaint evaluateComplaint)
        {
            _context.Evaluate_Complaints.Update(evaluateComplaint);
            await SaveAsync();
        }

        public async Task DeleteAsync(Evaluate_Complaint evaluateComplaint)
        {
            _context.Evaluate_Complaints.Remove(evaluateComplaint);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}