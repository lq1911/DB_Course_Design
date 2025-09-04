using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .Include(cm => cm.Coupons)   // 加载关联的优惠券集合
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
            try
            {
                // 记录要插入的数据
                Console.WriteLine($"准备插入优惠券数据:");
                Console.WriteLine($"- CouponManagerID: {couponManager.CouponManagerID} (类型: {couponManager.CouponManagerID.GetType()})");
                Console.WriteLine($"- CouponName: '{couponManager.CouponName}' (类型: {couponManager.CouponName?.GetType()}, 是否NULL: {couponManager.CouponName == null})");
                Console.WriteLine($"- CouponType: {couponManager.CouponType} (类型: {couponManager.CouponType.GetType()}, 是否NULL: {couponManager.CouponType == null})");
                Console.WriteLine($"- MinimumSpend: {couponManager.MinimumSpend} (类型: {couponManager.MinimumSpend.GetType()})");
                Console.WriteLine($"- DiscountAmount: {couponManager.DiscountAmount} (类型: {couponManager.DiscountAmount.GetType()})");
                Console.WriteLine($"- DiscountRate: {couponManager.DiscountRate} (类型: {couponManager.DiscountRate?.GetType()}, 是否NULL: {couponManager.DiscountRate == null})");
                Console.WriteLine($"- TotalQuantity: {couponManager.TotalQuantity} (类型: {couponManager.TotalQuantity.GetType()}, 是否NULL: {couponManager.TotalQuantity == null})");
                Console.WriteLine($"- UsedQuantity: {couponManager.UsedQuantity} (类型: {couponManager.UsedQuantity.GetType()}, 是否NULL: {couponManager.UsedQuantity == null})");
                Console.WriteLine($"- ValidFrom: {couponManager.ValidFrom} (类型: {couponManager.ValidFrom.GetType()})");
                Console.WriteLine($"- ValidTo: {couponManager.ValidTo} (类型: {couponManager.ValidTo.GetType()})");
                Console.WriteLine($"- Description: '{couponManager.Description}' (类型: {couponManager.Description?.GetType()}, 是否NULL: {couponManager.Description == null})");
                Console.WriteLine($"- StoreID: {couponManager.StoreID} (类型: {couponManager.StoreID.GetType()}, 是否NULL: {couponManager.StoreID == null})");
                Console.WriteLine($"- SellerID: {couponManager.SellerID} (类型: {couponManager.SellerID.GetType()}, 是否NULL: {couponManager.SellerID == null})");
                
                await _context.CouponManagers.AddAsync(couponManager);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"插入优惠券数据时发生错误: {ex.Message}");
                Console.WriteLine($"错误详情: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(CouponManager couponManager)
        {
            _context.CouponManagers.Update(couponManager);
            await SaveAsync();
        }

        public async Task DeleteAsync(CouponManager couponManager)
        {
            _context.CouponManagers.Remove(couponManager);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 根据商家ID获取优惠券列表（分页）
        /// </summary>
        public async Task<(IEnumerable<CouponManager> coupons, int total)> GetBySellerIdAsync(int sellerId, int page, int pageSize)
        {
            var query = _context.CouponManagers
                .Where(cm => cm.SellerID == sellerId)
                .OrderByDescending(cm => cm.CouponManagerID);

            var total = await query.CountAsync();
            var coupons = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (coupons, total);
        }

        /// <summary>
        /// 根据商家ID获取优惠券统计信息
        /// </summary>
        public async Task<(int total, int active, int expired, int upcoming, int totalUsed, decimal totalDiscountAmount)> GetStatsBySellerIdAsync(int sellerId)
        {
            var coupons = await _context.CouponManagers
                .Where(cm => cm.SellerID == sellerId)
                .ToListAsync();

            var total = coupons.Count;
            var now = DateTime.Now;
            
            var active = coupons.Count(c => c.ValidFrom <= now && c.ValidTo >= now);
            var expired = coupons.Count(c => c.ValidTo < now);
            var upcoming = coupons.Count(c => c.ValidFrom > now);
            var totalUsed = coupons.Sum(c => c.UsedQuantity);
            var totalDiscountAmount = coupons.Sum(c => c.DiscountAmount * c.UsedQuantity);

            return (total, active, expired, upcoming, totalUsed, totalDiscountAmount);
        }

        /// <summary>
        /// 根据商家ID和优惠券ID获取优惠券
        /// </summary>
        public async Task<CouponManager?> GetByIdAndSellerIdAsync(int id, int sellerId)
        {
            return await _context.CouponManagers
                .FirstOrDefaultAsync(cm => cm.CouponManagerID == id && cm.SellerID == sellerId);
        }

        /// <summary>
        /// 批量删除优惠券
        /// </summary>
        public async Task<int> BatchDeleteAsync(IEnumerable<int> ids, int sellerId)
        {
            var coupons = await _context.CouponManagers
                .Where(cm => ids.Contains(cm.CouponManagerID) && cm.SellerID == sellerId)
                .ToListAsync();

            _context.CouponManagers.RemoveRange(coupons);
            await SaveAsync();
            
            return coupons.Count;
        }
    }
}