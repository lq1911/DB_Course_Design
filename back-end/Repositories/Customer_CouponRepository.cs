using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Customer_CouponRepository : ICustomer_CouponRepository
    {
        private readonly AppDbContext _context;

        public Customer_CouponRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer_Coupon>> GetAllAsync()
        {
            // 预加载关联的 Customer 和 Coupon 数据
            return await _context.Customer_Coupon
                                 .Include(cc => cc.Customer)
                                 .Include(cc => cc.Coupon)
                                 .ToListAsync();
        }

        public async Task<Customer_Coupon?> GetByIdAsync(int customerId, int couponId)
        {
            // 对于复合主键，使用 FindAsync 并按顺序传入主键值
            return await _context.Customer_Coupon.FindAsync(customerId, couponId);
        }

        public async Task AddAsync(Customer_Coupon customerCoupon)
        {
            await _context.Customer_Coupon.AddAsync(customerCoupon);
        }

        public async Task UpdateAsync(Customer_Coupon customerCoupon)
        {
            _context.Customer_Coupon.Update(customerCoupon);
            await SaveAsync();
        }

        public Task DeleteAsync(Customer_Coupon customerCoupon)
        {
            _context.Customer_Coupon.Remove(customerCoupon);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}