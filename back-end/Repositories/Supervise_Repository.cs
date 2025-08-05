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
            return await _context.Set<Supervise_>().ToListAsync();
        }

        public async Task<Supervise_?> GetByIdAsync(int id)
        {
            return await _context.Set<Supervise_>().FindAsync(id);
        }

        public async Task AddAsync(Supervise_ supervise_)
        {
            _context.Set<Supervise_>().Add(supervise_);
            await SaveAsync();
        }

        public async Task UpdateAsync(Supervise_ supervise_)
        {
            _context.Set<Supervise_>().Update(supervise_);
            await SaveAsync();
        }

        public async Task DeleteAsync(Supervise_ supervise_)
        {
            _context.Set<Supervise_>().Remove(supervise_);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}