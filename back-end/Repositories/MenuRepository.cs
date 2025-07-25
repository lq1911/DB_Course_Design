using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;
        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Set<Menu>().ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Set<Menu>().FindAsync(id);
        }

        public async Task AddAsync(Menu menu)
        {
            _context.Set<Menu>().Add(menu);
            await SaveAsync();
        }

        public async Task UpdateAsync(Menu menu)
        {
            _context.Set<Menu>().Update(menu);
            await SaveAsync();
        }

        public async Task DeleteAsync(Menu menu)
        {
            _context.Set<Menu>().Remove(menu);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}