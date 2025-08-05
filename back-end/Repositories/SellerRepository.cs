using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AppDbContext _context;
        public SellerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seller>> GetAllAsync()
        {
            return await _context.Set<Seller>().ToListAsync();
        }

        public async Task<Seller?> GetByIdAsync(int id)
        {
            return await _context.Set<Seller>().FindAsync(id);
        }

        public async Task AddAsync(Seller seller)
        {
            _context.Set<Seller>().Add(seller);
            await SaveAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            _context.Set<Seller>().Update(seller);
            await SaveAsync();
        }

        public async Task DeleteAsync(Seller seller)
        {
            _context.Set<Seller>().Remove(seller);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}