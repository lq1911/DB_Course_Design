using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Supervise_Repository : ISupervise_Repository
    {
        private readonly AppDbContext _context;
        public Supervise_Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supervise_>> GetAllAsync()
        {
            return await _context.Supervise_s
                                 .Include(s => s.Admin)   // 管理员
                                 .Include(s => s.Penalty) // 处罚记录
                                 .ToListAsync();
        }

        // 复合主键查询
        public async Task<Supervise_?> GetByIdAsync(int adminId, int penaltyId)
        {
            return await _context.Supervise_s
                                 .Include(s => s.Admin)
                                 .Include(s => s.Penalty)
                                 .FirstOrDefaultAsync(s => s.AdminID == adminId && s.PenaltyID == penaltyId);
        }

        public async Task AddAsync(Supervise_ supervise_)
        {
            await _context.Supervise_s.AddAsync(supervise_);
            await SaveAsync();
        }

        public async Task UpdateAsync(Supervise_ supervise_)
        {
            _context.Supervise_s.Update(supervise_);
            await SaveAsync();
        }

        public async Task DeleteAsync(Supervise_ supervise_)
        {
            _context.Supervise_s.Remove(supervise_);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}