using BackEnd.Dtos.User;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;
namespace BackEnd.Services
{
    public class UserHomepageService : IUserHomepageService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IFoodOrderRepository _foodOrderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public UserHomepageService(
            IStoreRepository storeRepository,
            IUserRepository userRepository,
            ICouponRepository couponRepository,
            IFoodOrderRepository foodOrderRepository,
            IShoppingCartRepository shoppingCartRepository
            )
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
            _couponRepository = couponRepository;
            _foodOrderRepository = foodOrderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<HomeRecmDto> GetRecommendedStoresAsync()
        {
            // 【优化前】
            // var stores = await _storeRepository.GetAllAsync();
            // ... 在内存中排序和筛选 ...

            // 【优化后】直接从数据库获取已排序和限制数量的结果
            var topStores = await _storeRepository.GetTopRatedStoresForHomepageAsync(10);

            // 在内存中进行随机化是OK的，因为数据量已经很小 (只有10条)
            var random = new Random();
            var recommended = topStores
                .OrderBy(s => random.Next())
                .Take(4);

            return new HomeRecmDto
            {
                RecomStore = recommended
            };
        }

        public async Task<(IEnumerable<HomeSearchGetDto> Stores, IEnumerable<HomeSearchGetDto> Dishes)>
            SearchAsync(HomeSearchDto searchDto)
        {
            // 【优化前】加载了所有店铺和所有菜品到内存
            // var stores = await _storeRepository.GetAllAsync();
            // var dishes = await _dishRepository.GetAllAsync(); ...

            // 【优化后】让数据库执行搜索
            var storeResults = await _storeRepository.SearchStoresByNameAsync(searchDto.Keyword);
            var dishResults = await _storeRepository.SearchStoresByDishNameAsync(searchDto.Keyword);

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
                var store = await _storeRepository.GetStoreInfoForUserAsync(order.StoreID);

                // 获取购物车信息（如果存在）
                List<string> dishImages = new List<string>();
                decimal totalAmount = 0;

                if (order.CartID.HasValue)
                {
                    var cart = await _shoppingCartRepository.GetByIdAsync(order.CartID.Value);

                    if (cart != null && cart.ShoppingCartItems != null)
                    {
                        // 获取所有菜品图片
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
                    OrderStatus = order.FoodOrderState
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
            // 【优化前】
            // var stores = await _storeRepository.GetAllAsync();
            // ... 在内存中过滤 ...

            // 【优化后】直接从数据库获取所有运营中的店铺
            var operationalStores = await _storeRepository.GetOperationalStoresAsync();

            return new StoresResponseDto { AllStores = operationalStores.ToList() };
        }
    }

}

