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
            // 修正：使用 AppDbContext 中定义的正确属性名 "Customer_Coupons"
            return await _context.Customer_Coupons
                                 .Include(cc => cc.Customer)
                                 .Include(cc => cc.Coupon)
                                 .ToListAsync();
        }

        public async Task<Customer_Coupon?> GetByIdAsync(int customerId, int couponId)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "Customer_Coupons"
            return await _context.Customer_Coupons.FindAsync(customerId, couponId);
        }

        public async Task AddAsync(Customer_Coupon customerCoupon)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "Customer_Coupons"
            await _context.Customer_Coupons.AddAsync(customerCoupon);
        }

        public async Task UpdateAsync(Customer_Coupon customerCoupon)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "Customer_Coupons"
            _context.Customer_Coupons.Update(customerCoupon);
            await SaveAsync();
        }



        public Task DeleteAsync(Customer_Coupon customerCoupon)
        {
            // 修正：使用 AppDbContext 中定义的正确属性名 "Customer_Coupons"
            _context.Customer_Coupons.Remove(customerCoupon);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}