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
                
                Console.WriteLine($"查询结果: {(store == null ? "null" : $"StoreID={store.StoreID}, Name={store.StoreName}, SellerID={store.SellerID}")}");
                
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
            try
            {
                Console.WriteLine($"=== Repository层: 根据商家ID获取商家信息，商家ID: {sellerId} ===");
                
                // 先尝试只获取Seller信息，不包含User
                var seller = await _context.Sellers
                    .FirstOrDefaultAsync(s => s.UserID == sellerId);
                
                if (seller == null)
                {
                    Console.WriteLine("未找到商家信息");
                    return null;
                }
                
                Console.WriteLine($"找到商家信息: UserID={seller.UserID}, ReputationPoints={seller.ReputationPoints}");
                
                // 然后单独获取User信息
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserID == sellerId);
                
                if (user == null)
                {
                    Console.WriteLine("未找到用户信息");
                    return null;
                }
                
                Console.WriteLine($"找到用户信息: UserID={user.UserID}, Username={user.Username}");
                
                // 手动设置导航属性
                seller.User = user;
                
                return seller;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取商家信息异常: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                throw;
            }
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
                Console.WriteLine($"=== Repository层: 开始更新店铺信息 ===");
                Console.WriteLine($"店铺ID: {store.StoreID}");
                Console.WriteLine($"店铺名称: {store.StoreName}");
                Console.WriteLine($"店铺地址: {store.StoreAddress}");
                Console.WriteLine($"营业开始时间: {store.OpenTime}");
                Console.WriteLine($"营业结束时间: {store.CloseTime}");
                Console.WriteLine($"店铺特色: {store.StoreFeatures}");
                Console.WriteLine($"商家ID: {store.SellerID}");
                
                _context.Stores.Update(store);
                Console.WriteLine("店铺信息已标记为更新状态");
                
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"数据库保存结果: {result} 行受影响");
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== Repository层: 更新店铺信息异常 ===");
                Console.WriteLine($"异常类型: {ex.GetType().Name}");
                Console.WriteLine($"异常消息: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                }
                
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
            var lastMonthOrders = await _context.FoodOrders
                .Where(o => o.StoreID == storeId &&
                           o.PaymentTime.HasValue &&
                           o.PaymentTime.Value.Month == lastMonth.Month &&
                           o.PaymentTime.Value.Year == lastMonth.Year)
                .CountAsync();

            // 如果上个月没有订单，则获取本月1日至今的订单数
            if (lastMonthOrders == 0)
            {
                var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                lastMonthOrders = await _context.FoodOrders
                    .Where(o => o.StoreID == storeId &&
                               o.PaymentTime.HasValue &&
                               o.PaymentTime.Value >= firstDayOfMonth)
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