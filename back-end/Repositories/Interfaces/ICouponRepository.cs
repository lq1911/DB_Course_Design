using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetAllAsync();
        Task<Coupon?> GetByIdAsync(int id);
        Task<IEnumerable<Coupon>> GetByCustomerIdAsync(int customerId);
        Task AddAsync(Coupon coupon);
        Task UpdateAsync(Coupon coupon);
        Task DeleteAsync(Coupon coupon);
        Task SaveAsync();
    }
}