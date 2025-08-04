using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly AppDbContext _context;

        // 通过构造函数注入数据库上下文
        public MerchantRepository(AppDbContext context)
        {
            _context = context;
        }

        // 根据商家ID获取店铺信息
        public async Task<Store?> GetStoreBySellerIdAsync(int sellerId)
        {
            try
            {
                Console.WriteLine($"=== Repository层: 根据商家ID获取店铺信息，商家ID: {sellerId} ===");
                
                var store = await _context.Stores
                    .FirstOrDefaultAsync(s => s.SellerID == sellerId);
                
                Console.WriteLine($"查询结果: {(store == null ? "null" : $"StoreID={store.StoreID}, Name={store.StoreName}")}");
                
                return store;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取店铺信息异常: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                throw;
            }
        }

        // 根据商家ID获取商家信息
        public async Task<Seller?> GetSellerByIdAsync(int sellerId)
        {
            return await _context.Sellers
                .Include(s => s.User)  // 包含用户信息
                .FirstOrDefaultAsync(s => s.UserID == sellerId);
        }

        // 根据商家ID获取用户信息
        public async Task<User?> GetUserBySellerIdAsync(int sellerId)
        {
            var seller = await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserID == sellerId);
            
            return seller?.User;
        }

        // 更新店铺信息
        public async Task<bool> UpdateStoreAsync(Store store)
        {
            try
            {
                _context.Stores.Update(store);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 获取店铺评分（根据订单评价计算）
        public async Task<decimal> GetStoreRatingAsync(int storeId)
        {
            // TODO: 需要根据实际的评价表来计算评分
            // 暂时返回0，表示没有评分数据
            return 0m;
        }

        // 获取店铺月销量
        public async Task<int> GetStoreMonthlySalesAsync(int storeId)
        {
            var currentDate = DateTime.Now;
            var lastMonth = currentDate.AddMonths(-1);
            
            // 先尝试获取上个月的订单数
            var lastMonthOrders = await _context.Orders
                .Where(o => o.StoreID == storeId &&
                           o.PaymentTime.Month == lastMonth.Month &&
                           o.PaymentTime.Year == lastMonth.Year)
                .CountAsync();

            // 如果上个月没有订单，则获取本月1日至今的订单数
            if (lastMonthOrders == 0)
            {
                var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                lastMonthOrders = await _context.Orders
                    .Where(o => o.StoreID == storeId &&
                               o.PaymentTime >= firstDayOfMonth)
                    .CountAsync();
            }

            return lastMonthOrders;
        }

        // 保存操作
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 