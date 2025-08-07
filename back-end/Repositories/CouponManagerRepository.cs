using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class CouponManagerRepository : ICouponManagerRepository
    {
        private readonly AppDbContext _context;

        public CouponManagerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CouponManager>> GetAllAsync()
        {
            return await _context.CouponManagers
                .Include(cm => cm.Store)     // 加载关联的店铺
                .Include(cm => cm.Coupons)    // 加载关联的优惠券集合
                .ToListAsync();
        }

        public async Task<CouponManager?> GetByIdAsync(int id)
        {
            return await _context.CouponManagers
                .Include(cm => cm.Store)
                .Include(cm => cm.Coupons)
                .FirstOrDefaultAsync(cm => cm.CouponManagerID == id);
        }

        public async Task AddAsync(CouponManager couponManager)
        {
            await _context.CouponManagers.AddAsync(couponManager);
        }

        public async Task UpdateAsync(CouponManager couponManager)
        {
            _context.Entry(couponManager).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var couponManager = await _context.CouponManagers.FindAsync(id);
            if (couponManager != null)
            {
                _context.CouponManagers.Remove(couponManager);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}