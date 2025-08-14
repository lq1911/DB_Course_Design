using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface ICouponManagerRepository
    {
        Task<IEnumerable<CouponManager>> GetAllAsync();
        Task<CouponManager?> GetByIdAsync(int id);
        Task AddAsync(CouponManager couponManager);
        Task UpdateAsync(CouponManager couponManager);
        Task DeleteAsync(CouponManager couponManager);
        Task SaveAsync();


    }
}