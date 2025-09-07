using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.User;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models.Enums;
using BackEnd.Models;
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
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public UserHomepageService(
            IStoreRepository storeRepository,
            IDishRepository dishRepository,
            IMenuRepository menuRepository,
            IMenu_DishRepository menuDishRepository,
            IUserRepository userRepository,
            ICouponRepository couponRepository,
            IFoodOrderRepository foodOrderRepository,
            IShoppingCartRepository shoppingCartRepository
            )
        {
            _storeRepository = storeRepository;
            _dishRepository = dishRepository;
            _menuRepository = menuRepository;
            _menuDishRepository = menuDishRepository;
            _userRepository = userRepository;
            _couponRepository = couponRepository;
            _foodOrderRepository = foodOrderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<HomeRecmDto> GetRecommendedStoresAsync()
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
                .Select(s => new ShowStoreDto
                {
                    Id = s.StoreID,
                    Image = s.StoreImage,
                    Name = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                });

            return new HomeRecmDto
            {
                RecomStore = recommended
            };
        }

        public async Task<(IEnumerable<HomeSearchGetDto> Stores, IEnumerable<HomeSearchGetDto> Dishes)>
            SearchAsync(HomeSearchDto searchDto)
        {
            // 店铺搜索

            var stores = await _storeRepository.GetAllAsync();
            var storeResults = stores
                .Where(s => s.StoreName.Contains(searchDto.Keyword))
                .Select(s => new HomeSearchGetDto
                {
                    Id = s.StoreID,
                    Image = s.StoreImage,
                    Name = s.StoreName,
                    AverageRating = s.AverageRating,
                    MonthlySales = s.MonthlySales
                })
                .ToList();

            // 菜品搜索
            var dishes = await _dishRepository.GetAllAsync();
            var menus = await _menuRepository.GetAllAsync();
            var menuDishes = await _menuDishRepository.GetAllAsync();

            var dishResults = dishes
                .Where(d => d.DishName.Contains(searchDto.Keyword))
                .SelectMany(d => d.MenuDishes)          // Dish → Menu_Dish
                .Select(md => md.Menu.Store)            // Menu_Dish → Menu → Store
                .Distinct()                             // 避免重复
                .Select(store => new HomeSearchGetDto
                {
                    Id = store.StoreID,
                    Image = store.StoreImage,
                    Name = store.StoreName,
                    AverageRating = store.AverageRating,
                    MonthlySales = store.MonthlySales
                })
                .ToList();

            return (storeResults, dishResults);
        }
        public async Task<List<HistoryOrderDto>> GetOrderHistoryAsync(int userId)
        {
            // 获取用户的所有订单
            var orders = await _foodOrderRepository.GetOrdersByCustomerIdOrderedByDateAsync(userId);
            

            var result = new List<HistoryOrderDto>();

            foreach (var order in orders)
            {
                // 获取店铺信息
                var store = await _storeRepository.GetByIdAsync(order.StoreID);
                
                // 获取购物车信息（如果存在）
                List<string> dishImages = new List<string>();
                decimal totalAmount = 0;
                
                if (order.CartID.HasValue)
                {
                    var cart = await _shoppingCartRepository.GetByIdAsync(order.CartID.Value);

                    if (cart != null && cart.ShoppingCartItems != null)
                    {
                        // 获取所有菜品图片 - 修正后的逻辑
                dishImages = cart.ShoppingCartItems
                    .Where(sci => sci.Dish != null && !string.IsNullOrEmpty(sci.Dish.DishImage))
                    .Select(sci => sci.Dish.DishImage)
                    .OfType<string>() // 过滤掉 null 值
                    .Distinct()
                    .ToList();
                    
                // 计算总金额 - 修正后的逻辑
                totalAmount = cart.ShoppingCartItems
                    .Where(sci => sci.Dish != null)
                    .Sum(sci => sci.Quantity * sci.Dish.Price);
                    }
                }

                result.Add(new HistoryOrderDto
                {
                    OrderID = order.OrderID,
                    PaymentTime = order.PaymentTime.HasValue ?
                        order.PaymentTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    CartID = order.CartID ?? 0,
                    StoreID = order.StoreID,
                    StoreImage = store?.StoreImage ?? "",
                    StoreName = store?.StoreName ?? "",
                    DishImage = dishImages,
                    TotalAmount = totalAmount,
                    OrderStatus = (int)order.FoodOrderState
                });
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
                Name = user.Username,
                PhoneNumber = user.PhoneNumber,
                Image = user.Avatar
            };
        }


        // 查询用户优惠券（带 CouponManager 信息）
        public async Task<IEnumerable<CouponDto>> GetUserCouponsAsync(UserIdDto userIdDto)
        {
            var coupons = await _couponRepository.GetAllAsync();

            var results = coupons
                .Where(c => c.Customer.UserID == userIdDto.UserId)   // 过滤用户
                .Select(c => new CouponDto
                {
                    CouponID = c.CouponID,
                    CouponState = c.CouponState,
                    OrderID = c.OrderID,
                    CouponManagerID = c.CouponManagerID,

                    MinimumSpend = c.CouponManager.MinimumSpend,
                    DiscountAmount = c.CouponManager.DiscountAmount,
                    ValidTo = c.CouponManager.ValidTo
                });

            return results;
        }
        public async Task<StoresResponseDto> GetAllStoresAsync()
        {
            // 获取所有运营中的店铺
            var stores = await _storeRepository.GetAllAsync();
            var operationalStores = stores
                .Where(s => s.StoreState == StoreState.IsOperation)
                .Select(s => new ShowStoreDto
                {
                    Id = s.StoreID,
                    Image = s.StoreImage ?? "",
                    AverageRating = s.AverageRating,
                    Name = s.StoreName,
                    MonthlySales = s.MonthlySales
                })
                .ToList();

            return new StoresResponseDto { AllStores = operationalStores };
        }
    }

}

        