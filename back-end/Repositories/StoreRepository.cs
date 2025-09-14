using BackEnd.Data;
using BackEnd.Dtos.User;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Stores
                                 .Include(s => s.Seller)
                                 .Include(s => s.FoodOrders)
                                 .Include(s => s.CouponManagers)
                                 .Include(s => s.Menus)
                                 .Include(s => s.FavoriteItems)
                                 .Include(s => s.StoreViolationPenalties)
                                 .Include(s => s.Comments)
                                 .Include(s => s.DeliveryTasks)
                                 .Include(s => s.ShoppingCarts)
                                 .ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(int id)
        {
            return await _context.Stores
                                 .Include(s => s.Seller)
                                 .Include(s => s.FoodOrders)
                                 .Include(s => s.CouponManagers)
                                 .Include(s => s.Menus!)
                                     .ThenInclude(m => m.MenuDishes)
                                        .ThenInclude(md => md.Dish)
                                 .Include(s => s.FavoriteItems)
                                 .Include(s => s.StoreViolationPenalties)
                                 .Include(s => s.Comments)
                                 .Include(s => s.DeliveryTasks)
                                 .Include(s => s.ShoppingCarts)
                                 .FirstOrDefaultAsync(s => s.StoreID == id);
        }

        public async Task<Store?> GetBySellerIdAsync(int sellerId)
        {
            return await _context.Stores
                                 .Include(s => s.Seller)
                                 .Include(s => s.FoodOrders)
                                 .Include(s => s.CouponManagers)
                                 .Include(s => s.Menus!)
                                     .ThenInclude(m => m.MenuDishes)
                                        .ThenInclude(md => md.Dish)
                                 .Include(s => s.FavoriteItems)
                                 .Include(s => s.StoreViolationPenalties)
                                 .Include(s => s.Comments)
                                 .Include(s => s.DeliveryTasks)
                                 .FirstOrDefaultAsync(s => s.SellerID == sellerId);
        }

        public async Task<int?> GetStoreIdBySellerIdAsync(int sellerId)
        {
            return await _context.Stores
                                .AsNoTracking()
                                .Where(s => s.SellerID == sellerId)
                                .Select(s => s.StoreID)
                                .FirstOrDefaultAsync();
        }

        public async Task<Store?> GetStoreInfoForUserAsync(int storeId)
        {
            // 这个查询非常简单和快速，因为它没有加载任何关联数据
            return await _context.Stores
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(s => s.StoreID == storeId);
        }

        public async Task<IEnumerable<Dish>> GetDishesByStoreIdAsync(int storeId)
        {
            // 高效的投影查询，直接从数据库获取菜品列表
            return await _context.Menus
                .Where(m => m.StoreID == storeId)
                .SelectMany(m => m.MenuDishes)
                .Select(md => md.Dish)
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Store store)
        {
            await _context.Stores.AddAsync(store);
            await SaveAsync();
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Stores.Update(store);
            await SaveAsync();
        }

        public async Task DeleteAsync(Store store)
        {
            _context.Stores.Remove(store);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowStoreDto>> GetTopRatedStoresForHomepageAsync(int takeCount)
        {
            return await _context.Stores
                .AsNoTracking() // 提高只读查询性能
                .OrderByDescending(s => s.AverageRating)
                .Take(takeCount)
                .Select(s => new ShowStoreDto // 直接投影到 DTO
                {
                    Id = s.StoreID,
                    Image = s.StoreImage,
                    Name = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HomeSearchGetDto>> SearchStoresByNameAsync(string keyword)
        {
            return await _context.Stores
                .AsNoTracking()
                .Where(s => s.StoreName.Contains(keyword)) // 在数据库中过滤
                .Select(s => new HomeSearchGetDto // 直接投影
                {
                    Id = s.StoreID,
                    Image = s.StoreImage,
                    Name = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<HomeSearchGetDto>> SearchStoresByDishNameAsync(string keyword)
        {
            // 这个查询会找到所有菜品名包含关键字的店铺，且不重复
            return await _context.Dishes
                .AsNoTracking()
                .Where(d => d.DishName.Contains(keyword))
                .SelectMany(d => d.MenuDishes.Select(md => md.Menu.Store))
                .Distinct() // 关键：去重
                .Select(store => new HomeSearchGetDto
                {
                    Id = store.StoreID,
                    Image = store.StoreImage,
                    Name = store.StoreName,
                    AverageRating = store.AverageRating,
                    MonthlySales = store.MonthlySales
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ShowStoreDto>> GetOperationalStoresAsync()
        {
            return await _context.Stores
                .AsNoTracking()
                .Where(s => s.StoreState == StoreState.IsOperation) // 在数据库中过滤
                .Select(s => new ShowStoreDto
                {
                    Id = s.StoreID,
                    Image = s.StoreImage ?? "",
                    AverageRating = s.AverageRating,
                    Name = s.StoreName,
                    MonthlySales = s.MonthlySales
                })
                .ToListAsync();
        }
    }
}