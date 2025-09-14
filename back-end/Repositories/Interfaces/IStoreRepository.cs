using BackEnd.Dtos.User;
using BackEnd.Models;

namespace BackEnd.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        // 效率很差，不建议使用
        Task<IEnumerable<Store>> GetAllAsync();
        // 效率很差，不建议使用
        Task<Store?> GetByIdAsync(int id);
        Task<Store?> GetBySellerIdAsync(int sellerId);
        Task<int?> GetStoreIdBySellerIdAsync(int sellerId);
        Task<Store?> GetStoreInfoForUserAsync(int storeId);
        Task<IEnumerable<Dish>> GetDishesByStoreIdAsync(int storeId);
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(Store store);
        Task SaveAsync();
        // 【新增】为 Homepage 专门设计的高效查询方法
        Task<IEnumerable<ShowStoreDto>> GetTopRatedStoresForHomepageAsync(int takeCount);
        Task<IEnumerable<HomeSearchGetDto>> SearchStoresByNameAsync(string keyword);
        Task<IEnumerable<HomeSearchGetDto>> SearchStoresByDishNameAsync(string keyword);
        Task<IEnumerable<ShowStoreDto>> GetOperationalStoresAsync();
    }
}