using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Sellers
                                 .Include(s => s.User)   // 关联用户信息
                                 .Include(s => s.Stores)  // 关联店铺信息
                                 .ToListAsync();
        }

        public async Task<Seller?> GetByIdAsync(int id)
        {
            return await _context.Sellers
                                 .Include(s => s.User)
                                 .Include(s => s.Stores)
                                 .FirstOrDefaultAsync(s => s.UserID == id);
        }

        public async Task AddAsync(Seller seller)
        {
            await _context.Sellers.AddAsync(seller);
            await SaveAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            _context.Sellers.Update(seller);
            await SaveAsync();
        }

        public async Task DeleteAsync(Seller seller)
        {
            _context.Sellers.Remove(seller);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}