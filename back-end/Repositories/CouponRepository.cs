using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _context;

        public CouponRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coupon>> GetAllAsync()
        {
            // 预加载所有关联的实体数据
            return await _context.Coupons
                                 .Include(c => c.CouponManager)
                                 .Include(c => c.Order)
                                 .Include(c => c.Customer)
                                 .ToListAsync();
        }

        public async Task<Coupon?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.Coupons
                                 .Include(c => c.CouponManager)
                                 .Include(c => c.Order)
                                 .Include(c => c.Customer)
                                 .FirstOrDefaultAsync(c => c.CouponID == id);
        }
        public async Task<IEnumerable<Coupon>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Coupons
                                 .Include(c => c.CouponManager)
                                 .Include(c => c.Order)
                                 .Include(c => c.Customer)
                                 .Where(c => c.CustomerID == customerId)
                                 .ToListAsync();
        }

        public async Task AddAsync(Coupon coupon)
        {
            await _context.Coupons.AddAsync(coupon);
        }

        public async Task UpdateAsync(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await SaveAsync();
        }

        public Task DeleteAsync(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}