using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return await _context.Evaluate_Complaint
                                 .Include(ec => ec.Admin)
                                 .Include(ec => ec.Complaint)
                                 .ToListAsync();
        }

        public async Task<Evaluate_Complaint?> GetByIdAsync(int adminId, int complaintId)
        {
            // 对于复合主键，使用 FindAsync 并按顺序传入主键值
            return await _context.Evaluate_Complaint.FindAsync(adminId, complaintId);
        }

        public async Task AddAsync(Evaluate_Complaint evaluateComplaint)
        {
            await _context.Evaluate_Complaint.AddAsync(evaluateComplaint);
        }

        public async Task UpdateAsync(Evaluate_Complaint evaluateComplaint)
        {
            _context.Evaluate_Complaint.Update(evaluateComplaint);
            await SaveAsync();
        }

        public Task DeleteAsync(Evaluate_Complaint evaluateComplaint)
        {
            _context.Evaluate_Complaint.Remove(evaluateComplaint);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}