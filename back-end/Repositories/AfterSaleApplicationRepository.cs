using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class AfterSaleApplicationRepository : IAfterSaleApplicationRepository
    {
        private readonly AppDbContext _context;

        public AfterSaleApplicationRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AfterSaleApplication>> GetAllAsync()
        {
            return await _context.AfterSaleApplications
                                 .Include(a => a.Order)
                                 .Include(a => a.EvaluateAfterSales)
                                     .ThenInclude(eas => eas.Admin)
                                 .ToListAsync();
        }

        public async Task<AfterSaleApplication?> GetByIdAsync(int id)
        {
            return await _context.AfterSaleApplications
                                 .Include(a => a.Order)
                                 .Include(a => a.EvaluateAfterSales)
                                     .ThenInclude(eas => eas.Admin)
                                 .FirstOrDefaultAsync(a => a.ApplicationID == id);
        }

        public async Task AddAsync(AfterSaleApplication application)
        {
            await _context.AfterSaleApplications.AddAsync(application);
        }

        public async Task UpdateAsync(AfterSaleApplication application)
        {
            _context.AfterSaleApplications.Update(application);
            await SaveAsync();
        }

        public async Task DeleteAsync(AfterSaleApplication application)
        {
            _context.AfterSaleApplications.Remove(application);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}