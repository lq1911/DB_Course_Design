using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Publish_TaskRepository : IPublish_TaskRepository
    {
        private readonly AppDbContext _context;
        public Publish_TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publish_Task>> GetAllAsync()
        {
            return await _context.Set<Publish_Task>().ToListAsync();
        }

        public async Task<Publish_Task?> GetByIdAsync(int id)
        {
            return await _context.Set<Publish_Task>().FindAsync(id);
        }

        public async Task AddAsync(Publish_Task publish_task)
        {
            _context.Set<Publish_Task>().Add(publish_task);
            await SaveAsync();
        }

        public async Task UpdateAsync(Publish_Task publish_task)
        {
            _context.Set<Publish_Task>().Update(publish_task);
            await SaveAsync();
        }

        public async Task DeleteAsync(Publish_Task publish_task)
        {
            _context.Set<Publish_Task>().Remove(publish_task);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}