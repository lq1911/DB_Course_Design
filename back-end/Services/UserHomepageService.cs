using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.UserHomepage;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Services
{
    public class UserHomepageService : IUserHomepageService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenu_DishRepository _menuDishRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IFoodOrderRepository _foodOrderRepository;

        public UserHomepageService(
            IStoreRepository storeRepository,
            IDishRepository dishRepository,
            IMenuRepository menuRepository,
            IMenu_DishRepository menuDishRepository,
            IUserRepository userRepository,
            ICouponRepository couponRepository,
            IFoodOrderRepository foodOrderRepository
            )
        {
            _storeRepository = storeRepository;
            _dishRepository = dishRepository;
            _menuRepository = menuRepository;
            _menuDishRepository = menuDishRepository;
            _userRepository = userRepository;
            _couponRepository = couponRepository;
            _foodOrderRepository = foodOrderRepository;
        }

        public async Task<IEnumerable<HomeRecmDto>> GetRecommendedStoresAsync()
        {
            var stores = await _storeRepository.GetAllAsync();

            // 按评分排序，取前10个，然后随机选4个
            var topStores = stores
                .OrderByDescending(s => s.AverageRating)
                .Take(10)
                .ToList();

            var random = new Random();
            var recommended = topStores
                .OrderBy(s => random.Next())
                .Take(4)
                .Select(s => new HomeRecmDto
                {
                    StoreID = s.StoreID,
                    StoreName = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                });

            return recommended;
        }

        public async Task<(IEnumerable<HomeSearchGetDto> Stores, IEnumerable<HomeSearchGetDto> Dishes)>
            SearchAsync(HomeSearchDto searchDto)
        {
            // 店铺搜索
            
             var stores = await _storeRepository.GetAllAsync();
            var storeResults = stores
                .Where(s => s.StoreName.Contains(searchDto.SearchName))
                .Select(s => new HomeSearchGetDto
                {
                    AverageRating = s.AverageRating,
                    StoreName = s.StoreName,
                    MonthlySales = s.MonthlySales,
                    StoreAddress = s.StoreAddress
                })
                .ToList();

            // 菜品搜索
            var dishes = await _dishRepository.GetAllAsync();
            var menus = await _menuRepository.GetAllAsync();
            var menuDishes = await _menuDishRepository.GetAllAsync();

            var dishResults = dishes
                .Where(d => d.DishName.Contains(searchDto.SearchName))
                .SelectMany(d => d.MenuDishes)          // Dish → Menu_Dish
                .Select(md => md.Menu.Store)            // Menu_Dish → Menu → Store
                .Distinct()                             // 避免重复
                .Select(store => new HomeSearchGetDto
                {
                    AverageRating = store.AverageRating,
                    StoreName = store.StoreName,
                    MonthlySales = store.MonthlySales,
                    StoreAddress = store.StoreAddress
                })
                .ToList();

            return (storeResults, dishResults);
        }
        public async Task<HistoryOrderGetDto> GetOrderHistoryAsync(int userId)
        {
            // 查询用户的所有订单，包含相关数据
            var orders = await _foodOrderRepository.GetAllAsync(userId);

            // 转换为DTO
            var result = new HistoryOrderGetDto();

            foreach (var order in orders)
            {
                var orderDto = new HistoryOrderDto
                {
                    OrderID = order.OrderID,
                    PaymentTime = order.PaymentTime,
                    CartID = order.CartID,
                    StoreID = order.StoreID,
                    StoreName = order.Store?.StoreName ?? "未知商家",
                    //StoreImage = order.Store?.ImageUrl
                };
                result.Orders.Add(orderDto);
            }

            return result;
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return null;

            return new UserInfoResponse
            {
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar
            };
        }


        // 查询用户优惠券（带 CouponManager 信息）
        public async Task<IEnumerable<CouponDto>> GetUserCouponsAsync(UserIdDto userIdDto)
        {
            var coupons = await _context.Coupons
                .AsNoTracking()
                .Where(c => c.Customer.UserID == userIdDto.UserId)   // 过滤用户
                .Include(c => c.CouponManager)                       // 关联 CouponManager
                .Select(c => new CouponDto
                {
                    CouponID = c.CouponID,
                    CouponState = c.CouponState,
                    OrderID = c.OrderID,
                    CouponManagerID = c.CouponManagerID,

                    MinimumSpend = c.CouponManager.MinimumSpend,
                    DiscountAmount = c.CouponManager.DiscountAmount,
                    ValidTo = c.CouponManager.ValidTo
                })
                .ToListAsync();

            return coupons;
        }
    }

}

        