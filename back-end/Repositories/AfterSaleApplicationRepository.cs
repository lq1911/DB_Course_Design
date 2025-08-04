using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            // 修正：使用 AppDbContext 中定义的正确属性名 "AfterSaleApplications"
            return await _context.AfterSaleApplications
                                 .Include(a => a.Customer)
                                 .Include(a => a.Order)
                                 .ToListAsync();
        }

        public async Task<AfterSaleApplication?> GetByIdAsync(int id)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "AfterSaleApplications"
            return await _context.AfterSaleApplications
                                 .Include(a => a.Customer)
                                 .Include(a => a.Order)
                                 .FirstOrDefaultAsync(a => a.ApplicationID == id);
        }

        public async Task AddAsync(AfterSaleApplication application)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "AfterSaleApplications"
            await _context.AfterSaleApplications.AddAsync(application);
        }

        public async Task UpdateAsync(AfterSaleApplication application)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "AfterSaleApplications"
            _context.AfterSaleApplications.Update(application);
            await SaveAsync();
        }

        public Task DeleteAsync(AfterSaleApplication application)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "AfterSaleApplications"
            _context.AfterSaleApplications.Remove(application);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}