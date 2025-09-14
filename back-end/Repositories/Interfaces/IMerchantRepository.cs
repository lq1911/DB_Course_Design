using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IMerchantRepository
    {
        // 根据商家ID获取店铺信息
        Task<Store?> GetStoreBySellerIdAsync(int sellerId);
        
        // 根据商家ID获取商家信息
        Task<Seller?> GetSellerByIdAsync(int sellerId);
        
        // 根据商家ID获取用户信息
        Task<User?> GetUserBySellerIdAsync(int sellerId);
        
        // 更新店铺信息
        Task<bool> UpdateStoreAsync(Store store);
        
        // 获取店铺评分（根据订单评价计算）
        Task<decimal> GetStoreRatingAsync(int storeId);
        
        // 获取店铺月销量
        Task<int> GetStoreMonthlySalesAsync(int storeId);
        
        // 保存操作
        Task SaveAsync();
    }
} 