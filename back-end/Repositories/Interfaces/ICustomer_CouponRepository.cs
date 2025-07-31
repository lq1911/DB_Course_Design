using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface ICustomer_CouponRepository
    {
        Task<IEnumerable<Customer_Coupon>> GetAllAsync();
        Task<Customer_Coupon?> GetByIdAsync(int customerId, int couponId);
        Task AddAsync(Customer_Coupon customerCoupon);
        Task UpdateAsync(Customer_Coupon customerCoupon);
        Task DeleteAsync(Customer_Coupon customerCoupon);
        Task SaveAsync();
    }
}