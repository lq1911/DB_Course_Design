using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface ICouponManagerRepository
    {
        Task<IEnumerable<CouponManager>> GetAllAsync();
        Task<CouponManager?> GetByIdAsync(int id);
        Task AddAsync(CouponManager couponManager);
        Task UpdateAsync(CouponManager couponManager);
        Task DeleteAsync(int id);
        Task SaveAsync();
        
        
    }
}