using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            return await _context.Administrators
                                 .Include(a => a.User)
                                 .Include(a => a.ReviewComments)
                                    .ThenInclude(rc => rc.Comment)
                                 .Include(a => a.Supervise_s)
                                      .ThenInclude(s => s.Penalty)
                                 .Include(a => a.EvaluateAfterSales)
                                      .ThenInclude(eas => eas.Application)
                                 .Include(a => a.EvaluateComplaints)
                                     .ThenInclude(ec => ec.Complaint)
                                 .ToListAsync();
        }

        public async Task<Administrator?> GetByIdAsync(int id)
        {
            return await _context.Administrators
                                 .Include(a => a.User)
                                 .Include(a => a.ReviewComments)
                                    .ThenInclude(rc => rc.Comment)
                                 .Include(a => a.Supervise_s)
                                      .ThenInclude(s => s.Penalty)
                                 .Include(a => a.EvaluateAfterSales)
                                      .ThenInclude(eas => eas.Application)
                                 .Include(a => a.EvaluateComplaints)
                                     .ThenInclude(ec => ec.Complaint)
                                 .FirstOrDefaultAsync(a => a.UserID == id);
        }

        public async Task<IEnumerable<AfterSaleApplication>> GetAfterSaleApplicationsByAdminIdAsync(int adminId)
        {
            var applications = await _context.Evaluate_AfterSales
                                             .Where(eas => eas.AdminID == adminId)
                                             .Select(eas => eas.Application)
                                             .ToListAsync();

            return applications;
        }

        public async Task AddAsync(Administrator administrator)
        {
            await _context.Administrators.AddAsync(administrator);
            await SaveAsync();
        }

        public async Task UpdateAsync(Administrator administrator)
        {
            _context.Administrators.Update(administrator);
            await SaveAsync();
        }

        public async Task DeleteAsync(Administrator administrator)
        {
            _context.Administrators.Remove(administrator);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}