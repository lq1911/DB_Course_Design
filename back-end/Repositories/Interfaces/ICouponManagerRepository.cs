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

        // 商家优惠券管理相关方法
        Task<(IEnumerable<CouponManager> coupons, int total)> GetByStoreIdAsync(int storeId, int page, int pageSize);
        Task<(int total, int active, int expired, int upcoming, int totalUsed, decimal totalDiscountAmount)> GetStatsByStoreIdAsync(int storeId);
        Task<CouponManager?> GetByIdAndStoreIdAsync(int id, int storeId);
        Task<int> BatchDeleteAsync(IEnumerable<int> ids, int storeId);
    }
}